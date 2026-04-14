using System;
using System.Collections.Generic;
using System.IO;

namespace CoDepend.Application;

public static class ExclusionManager
{
    public sealed record ExclusionRule(
        string[] DirPrefixes,
        string[] Segments,
        string[] FileSuffixes
    );

    public static ExclusionRule CompileExclusions(IReadOnlyList<string> exclusions)
    {
        List<string> dirPrefixes = [];
        List<string> segments = [];
        List<string> suffixes = [];

        foreach (var entry in exclusions)
        {
            var norm = NormalizeExclusionEntry(entry);
            if (norm is null) continue;

            if (ToDirPrefix(norm) is { } prefix)
            {
                dirPrefixes.Add(prefix);
                continue;
            }

            if (norm.StartsWith("*.", StringComparison.Ordinal))
            {
                suffixes.Add(norm[1..]);
                continue;
            }

            segments.Add(norm);
        }

        return new ExclusionRule(
            DirPrefixes: [.. dirPrefixes],
            Segments: [.. segments],
            FileSuffixes: [.. suffixes]
        );
    }

    public static string? NormalizeExclusionEntry(string? entry)
    {
        var exclusion = (entry ?? string.Empty).Trim();
        if (exclusion.Length == 0) return null;

        if (exclusion.StartsWith("**/", StringComparison.Ordinal))
            exclusion = exclusion[3..];

        var norm = exclusion.Replace('\\', '/');
        return norm.EndsWith('.') ? norm[..^1] : norm;
    }

    public static bool IsExcluded(string projectRoot, string content, ExclusionRule rules)
    {
        var path = GetRelative(projectRoot, content);

        var sep = '/';
        var pathWithSlash = path + sep;
        var pathWithBothSlashes = sep + path + sep;

        foreach (var rule in rules.DirPrefixes)
        {
            if (pathWithSlash.StartsWith(rule, StringComparison.OrdinalIgnoreCase) ||
                pathWithBothSlashes.Contains(sep + rule, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        var segments = path.Split(sep, StringSplitOptions.RemoveEmptyEntries);

        foreach (var segment in segments)
        {
            foreach (var ban in rules.Segments)
            {
                if (MatchesSuffixPattern(segment, ban))
                    return true;
            }
        }

        var fileName = Path.GetFileName(path);

        foreach (var suf in rules.FileSuffixes)
        {
            if (fileName.EndsWith(suf, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private static string? ToDirPrefix(string norm)
    {
        if (norm.EndsWith('/'))
        {
            var p = norm.StartsWith("./", StringComparison.Ordinal) ? norm[2..] : norm;
            return p.EndsWith('/') ? p : p + "/";
        }

        if (norm.Contains('/'))
            return norm + "/";

        return null;
    }

    private static bool MatchesSuffixPattern(string value, string pattern)
    {
        if (!pattern.Contains('*'))
            return string.Equals(value, pattern, StringComparison.OrdinalIgnoreCase);

        var suffix = pattern.TrimStart('*');
        return value.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
    }

    private static string GetRelative(string root, string path)
    {
        var rel = Path.GetRelativePath(root, path);
        return rel.Replace('\\', '/');
    }
}