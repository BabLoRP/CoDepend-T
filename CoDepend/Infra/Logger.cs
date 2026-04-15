using System;
using CoDepend.Domain.Interfaces;

namespace CoDepend.Infra;

public sealed class Logger : ILogger
{
    public void LogInformation(string message)
        => Console.WriteLine("INFO: " + message);

    public void LogWarning(string message)
        => Console.WriteLine("WARN: " + message);

    public void LogError(string message, Exception? ex = null)
        => Console.WriteLine($"ERROR: {message} {ex}");
}