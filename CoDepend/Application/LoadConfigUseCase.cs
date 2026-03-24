using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain;
using CoDepend.Domain.Interfaces;

namespace CoDepend.Application;

public sealed class LoadConfigUseCase(IConfigLoader configLoader)
{
    public async Task<ConfigManager> RunAsync(
        string configPath, bool diff = false, string format = "puml", CancellationToken ct = default)
    {
        var (baseOptions, parserOptions, renderOptions, snapshotOptions) =
            await configLoader.LoadAsync(configPath, diff, format, ct);

        return new ConfigManager(baseOptions, parserOptions, renderOptions, snapshotOptions);
    }
}
