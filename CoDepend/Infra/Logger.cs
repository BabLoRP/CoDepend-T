
using System;

namespace CoDepend.Infra;
public class Logger
{
    public void LogInformation(string input)
    {
        Console.WriteLine($"INFO: {input}");
    }

    public void LogWarning(string input)
    {
        Console.WriteLine($"WARN: {input}");
    }

    public void LogError(string input)
    {
        Console.Error.WriteLine($"ERROR: {input}");
    }
}