using System;
namespace CoDepend.Infra.Logger;

#pragma warning disable CA1050 // Declare types in namespaces
public class Logger
#pragma warning restore CA1050 // Declare types in namespaces
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