using CoDepend.Domain.Models;

namespace CoDepend.Infra;

public class Repository
{
    public ProjectDependencyGraph? GetSnapshot()
    {
        return null;
    }

    public void SetSnapshot(ProjectDependencyGraph snapshot)
    {
        // Currently does nothing
    }
}