using System;

namespace CoDepend.Infra;

public static class Logger
{
    public static void LogInformation(string input)
    {
        Console.WriteLine($"INFO: {input}");
    }

    public static void LogWarning(string input)
    {
        Console.WriteLine($"WARN: {input}");
    }

    public static void LogError(string input)
    {
        Console.WriteLine($"ERROR: {input}");
    }
}