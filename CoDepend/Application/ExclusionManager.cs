namespace CoDepend.Application;

public sealed class ExclusionManager
{
    private sealed record ExclusionRule(
        string[] DirPrefixes,
        string[] Segments,
        string[] FileSuffixes
    );

    public static bool IsExcluded()
    {
        return true;
    }

    public static void NormalizeExclusionEntry()
    {
        
    }

    public static void CompileExclusions()
    {
        
    }
}