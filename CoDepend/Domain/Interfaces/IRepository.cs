using CoDepend.Domain.Models;

namespace CoDepend.Domain.Interfaces;

public interface IRepository
{
    ProjectDependencyGraph? GetSnapshot();
    void SetSnapshot(ProjectDependencyGraph snapshot);
}