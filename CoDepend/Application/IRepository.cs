using CoDepend.Domain.Models;

namespace CoDepend.Application
{
    public interface IRepository
    {
        ProjectDependencyGraph? GetSnapshot();
        void SetSnapshot(ProjectDependencyGraph snapshot);
    }
}