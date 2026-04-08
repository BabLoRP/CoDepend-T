using CoDepend.Domain.Models;

namespace CoDepend.Infra;

public sealed class Repository
{
    public ProjectDependencyGraph? GetSnapshot()
    {
        return null;
    }

    public void SetSnapshot(ProjectDependencyGraph snapshot)
    {
        // Nothing for now.
    }
}
