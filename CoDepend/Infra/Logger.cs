
using System;

namespace CoDepend.Infra;

public static class Logger
{
    public static void LogInformation(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }

    public static void LogWarning(string message)
    {
        Console.WriteLine($"WARN: {message}");
    }

    public static void LogError(string message)
    {
        Console.WriteLine($"ERROR: {message}");
    }
}