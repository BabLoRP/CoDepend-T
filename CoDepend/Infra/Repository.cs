using CoDepend.Application;
using CoDepend.Domain.Models;

namespace CoDepend.Infra;

public class Repository : IRepository
{
    public ProjectDependencyGraph? GetSnapshot() => null;

    public void SetSnapshot(ProjectDependencyGraph snapshot) { }
}
