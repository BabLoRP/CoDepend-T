using System;

namespace CoDepend.Application;

public sealed class ExclusionManager
{
    
    private sealed record ExclusionRule(
        string[] DirPrefixes,
        string[] Segments,
        string[] FileSuffixes
    );

    public static IsExcluded() {

    }

    public static NormalizeExclusionEntry() {

    }

    public static CompileExclusions() {

    }

}

//The class contains the ExclusionRule record
//The class contains public methods:
//IsExcluded
//NormalizeExclusionEntry
//CompileExclusions