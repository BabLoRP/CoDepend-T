using CoDepend.Application;
using CoDepend.Domain.Models;

namespace CoDepend.Infra;

public class Repository : IRepository
{
    public ProjectDependencyGraph? GetSnapshot()
    {
        return null;
    }

    public void SetSnapshot(ProjectDependencyGraph snapshot)
    {
        _ = snapshot;
        // intentionally no-op for now
    }
}