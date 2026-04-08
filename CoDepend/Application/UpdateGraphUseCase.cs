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
    private readonly Logger _logger = new();

    public async Task RunAsync(CancellationToken ct = default)
    {
        _logger.LogInformation(diff ? "Running diff use case" : "Running non-diff use case");

        var snapshotGraph = await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct);
        var projectChanges = await ChangeDetector.GetProjectChangesAsync(parserOptions, snapshotGraph, ct);
        var graph = await new DependencyGraphBuilder(parsers, baseOptions).GetGraphAsync(projectChanges, snapshotGraph, ct);

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
