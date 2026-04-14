using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models;

namespace CoDepend.Infra
{
    public class Repository : IRepository
    {
        public ProjectDependencyGraph? GetSnapshot()
        {
            return null;
        }

        public void SetSnapshot(ProjectDependencyGraph snapshot)
        {
            // FIXME: does nothing for now
        }
    }
}