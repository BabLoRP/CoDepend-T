using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Infra.SnapshotManagers;

public sealed class LocalSnapshotManager(string _localDirName, string _localFileName) : ISnapshotManager
{
    public async Task SaveGraphAsync(ProjectDependencyGraph graph, SnapshotOptions options, CancellationToken ct = default)
    {
        var root = string.IsNullOrEmpty(options.BaseOptions.FullRootPath)
            ? Path.GetFullPath(options.BaseOptions.ProjectRoot)
            : options.BaseOptions.FullRootPath;

        var dir = Path.Combine(root, _localDirName);
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, _localFileName);
        var logger = new Logger();

        var bytes = DependencyGraphSerializer.Serialize(graph);
        if(bytes.Length == 0)
        {
            logger.LogWarning($"Size of the serialization was: {bytes.Length.ToString()}");
        }
        else
        {
            logger.LogInformation($"Size of the serialization was: {bytes.Length.ToString()}");
        }
        await File.WriteAllBytesAsync(path, bytes, ct);
    }

    public async Task<ProjectDependencyGraph?> GetLastSavedDependencyGraphAsync(SnapshotOptions options, CancellationToken ct = default)
    {
        var root = string.IsNullOrEmpty(options.BaseOptions.FullRootPath)
            ? Path.GetFullPath(options.BaseOptions.ProjectRoot)
            : options.BaseOptions.FullRootPath;

        var path = Path.Combine(root, _localDirName, _localFileName);

        if (!File.Exists(path))
            return null;

        var bytes = await File.ReadAllBytesAsync(path, ct);
        try
        {
            return DependencyGraphSerializer.Deserialize(bytes, root);
        }
        catch { return null; }
    }
}
