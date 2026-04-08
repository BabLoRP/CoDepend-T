using System;

namespace CoDepend.Infra;

public class Logger
{
    public static void LogInformation(string information)
    {
        Console.WriteLine($"INFO: {information}");
    }

    public static void LogWarning(string information)
    {
        Console.WriteLine($"WARN: {information}");
    }

    public static void LogError(string information)
    {
        Console.WriteLine($"ERROR: {information}");
    }
}