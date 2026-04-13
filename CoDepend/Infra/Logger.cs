using System;

class Logger
{
  public static void LogInformation(String input)
  {
    Console.WriteLine($"INFO: {input}");
  }

  public static void LogWarning(String input)
  {
    Console.WriteLine($"WARN: {input}");
  }

  public static void LogError(String input)
  {
    Console.Error.WriteLine($"ERROR: {input}");
  }
}