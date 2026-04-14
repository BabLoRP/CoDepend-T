using CoDepend.Domain.Interfaces;

namespace CoDependTests.Application;
public sealed class TestLogger : ILogger
{
    public List<string> Info = [];
    public List<string> Warnings = [];
    public List<string> Errors = [];

    public void LogInformation(string message) => Info.Add(message);
    public void LogWarning(string message) => Warnings.Add(message);
    public void LogError(string message) => Errors.Add(message);
}