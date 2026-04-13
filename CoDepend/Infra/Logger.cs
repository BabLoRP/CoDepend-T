using System;

namespace CoDepend.Infra;

public class Logger
{
    public void LogInformation(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }

    public void LogWarning(string message)
    {
        Console.WriteLine($"WARN: {message}");
    }

    public void LogError(string input)
    {
        var message = $"ERROR: {input}";
        Console.WriteLine(message);
        Console.Error.WriteLine(message);
    }
}