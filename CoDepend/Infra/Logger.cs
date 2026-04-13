using System;
using CoDepend.Application;

namespace CoDepend.Infra;

public class Logger : ILogger
{
    public void LogInformation(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"WARN: {message}");
    }

    public void LogError(string message)
    {
        Console.WriteLine($"ERROR: {message}");
    }
}