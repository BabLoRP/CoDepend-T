using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra;

namespace CoDepend.Application;

public sealed class UpdateGraphUseCase(
    BaseOptions baseOptions,
    ParserOptions parserOptions,
    RenderOptions renderOptions,
    SnapshotOptions snapshotOptions,
    IReadOnlyList<IDependencyParser> parsers,
    RendererBase renderer,
    ISnapshotManager snapshotManager,
    bool diff = false
    )
{
    public async Task RunAsync(CancellationToken ct = default)
    {
        var snapshotGraph = await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct);
        var projectChanges = await ChangeDetector.GetProjectChangesAsync(parserOptions, snapshotGraph, ct);
        var graph = await new DependencyGraphBuilder(parsers, baseOptions).GetGraphAsync(projectChanges, snapshotGraph, ct);

        if (renderOptions.Format != RenderFormat.None)
        {
            if (diff)
            {
                var compareGraph = await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct) ?? throw new InvalidOperationException("Diff mode requires a saved snapshot, but none was found.");
                Logger.LogInformation("Running diff use case");
                await renderer.RenderDiffViewsAndSaveToFiles(graph, compareGraph, renderOptions, ct);
            }
            else
            {
                await renderer.RenderViewsAndSaveToFiles(graph, renderOptions, ct);
                Logger.LogInformation("Running non-diff use case");
                
            }
        }

        await snapshotManager.SaveGraphAsync(graph, snapshotOptions, ct);
    }
}
