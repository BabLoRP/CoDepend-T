using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Enums;

namespace CoDepend.Application;

public sealed class UpdateGraphUseCase(
    ConfigManager configManager,
    IReadOnlyList<IDependencyParser> parsers,
    RendererBase renderer,
    ISnapshotManager snapshotManager,
    ILogger logger,
    bool diff = false,
    IRepository? repository = null
    )
{
    public async Task RunAsync(CancellationToken ct = default)
    {
        if (diff)
            logger.LogInformation("Running diff use case");
        else
            logger.LogInformation("Running non-diff use case");

        var baseOptions = configManager.GetBaseOptions();
        var parserOptions = configManager.GetParserOptions();
        var renderOptions = configManager.GetRenderOptions();
        var snapshotOptions = configManager.GetSnapshotOptions();

        var snapshotGraph = repository?.GetSnapshot()
            ?? await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct);
        var projectChanges = await ChangeDetector.GetProjectChangesAsync(parserOptions, snapshotGraph, ct);
        var graph = await new DependencyGraphBuilder(parsers, baseOptions, logger).GetGraphAsync(projectChanges, snapshotGraph, ct);

        if (renderOptions.Format != RenderFormat.None)
        {
            if (diff)
            {
                var compareGraph = await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct) ?? throw new InvalidOperationException("Diff mode requires a saved snapshot, but none was found.");
                await renderer.RenderDiffViewsAndSaveToFiles(graph, compareGraph, renderOptions, ct);
            }
            else
                await renderer.RenderViewsAndSaveToFiles(graph, renderOptions, ct);
        }

        await snapshotManager.SaveGraphAsync(graph, snapshotOptions, ct);
    }
}
