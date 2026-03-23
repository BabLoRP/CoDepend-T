using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain.Models;
using CoDepend.Domain.Models.Records;
namespace CoDepend.Domain.Interfaces;

public interface ISnapshotManager
{
    Task SaveGraphAsync(ProjectDependencyGraph graph,
                   SnapshotOptions options,
                   CancellationToken ct = default);
    Task<ProjectDependencyGraph?> GetLastSavedDependencyGraphAsync(SnapshotOptions options, CancellationToken ct = default);
}