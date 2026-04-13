using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain.Models;
using CoDepend.Domain.Models.Records;
using CoDepend.Domain.Utils;
using ProjectChanges = CoDepend.Domain.Models.Records.ProjectChanges;
using ProjectDependencyGraph = CoDepend.Domain.Models.ProjectDependencyGraph;

namespace CoDepend.Application;

public class ExclusionManager
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
            var norm = NormaliseExclusionEntry(entry);
            if (norm is null) continue;

            if (ToDirPrefix(norm) is { } prefix) { dirPrefixes.Add(prefix); continue; }
            if (norm.StartsWith("*.", StringComparison.Ordinal)) { suffixes.Add(norm[1..]); continue; }
            segments.Add(norm);
        }

        return new ExclusionRule(
            DirPrefixes: [.. dirPrefixes],
            Segments: [.. segments],
            FileSuffixes: [.. suffixes]
        );
    }

    // Returns the canonical dir-prefix form when norm represents a directory pattern, otherwise null.
    private static string? ToDirPrefix(string norm)
    {
        // relative path with trailing '/' -> dir
        if (norm.EndsWith('/'))
        {
            var p = norm.StartsWith("./", StringComparison.Ordinal) ? norm[2..] : norm;
            return p.EndsWith('/') ? p : p + "/";
        }

        // relative path containing '/' but no trailing slash -> dir
        if (norm.Contains('/'))
            return norm + "/";

        return null;
    }

        // Returns null for blank/empty entries; otherwise strips **/  prefix, normalises slashes, and trims trailing dot.
    private static string? NormaliseExclusionEntry(string? entry)
    {
        var exclusion = (entry ?? string.Empty).Trim();
        if (exclusion.Length == 0) return null;

        if (exclusion.StartsWith("**/", StringComparison.Ordinal)) exclusion = exclusion[3..];

        var norm = exclusion.Replace('\\', '/');
        return norm.EndsWith('.') ? norm[..^1] : norm;
    }

}