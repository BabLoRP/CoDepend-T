using System;

namespace CoDepend.Infra;

public static class Logger
{
    public static void LogInformation(string Info)
    {
        Console.WriteLine("INFO: " + Info);
    }

    public static void LogWarning(string Warning)
    {
        Console.WriteLine("WARN: " + Warning);
    }

    public static void LogError(string Error)
    {
        Console.WriteLine("ERROR: " + Error);
    }
}