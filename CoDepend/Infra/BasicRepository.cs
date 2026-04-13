using CoDepend.Domain.Models;

namespace CoDepend.Infra;

public class BasicRepository
{
    public ProjectDependencyGraph? GetSnapshot()
    {
        return null;
    }

    public void SetSnapshot(ProjectDependencyGraph snapshot)
    {
        // TODO: Implement snapshot persistence
    }
}