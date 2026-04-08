
using System;
using System.Threading.Tasks;

namespace CoDepend.Infra;

public class Logger
{
    public static void LogInformation(string input)
    {
        Console.Write($"INFO: {input}");
    }

    public static void LogWarning(string input)
    {
        Console.Write($"Warning: {input}");   
    }

    public static void LogError(string input)
    {
        Console.Write($"Error: {input}");
    }

    public static Task LogErrorAsync(string input)
    {
        return Console.Error.WriteLineAsync($"Error: {input}");
    }
}
