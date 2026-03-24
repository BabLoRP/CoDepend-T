using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Domain.Interfaces;

public interface IConfigLoader
{
    Task<(BaseOptions, ParserOptions, RenderOptions, SnapshotOptions)> LoadAsync(
        string path, bool diff = false, string format = "puml", CancellationToken ct = default);
}
