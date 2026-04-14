using CoDepend.Domain.Interfaces;

namespace CoDependTests.Performance;
sealed class NullLogger : ILogger
{
    public void LogInformation(string message) { }
    public void LogWarning(string message) { }
    public void LogError(string message) { }
}