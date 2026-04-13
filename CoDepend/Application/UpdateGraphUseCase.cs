using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Application;

public sealed class UpdateGraphUseCase(
    BaseOptions baseOptions,
    ParserOptions parserOptions,
    RenderOptions renderOptions,
    SnapshotOptions snapshotOptions,
    IReadOnlyList<IDependencyParser> parsers,
    RendererBase renderer,
    ISnapshotManager snapshotManager,
    IRepository _repository = null!,
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
                var snapshotFromRepository = _repository?.GetSnapshot();
                var compareGraph = snapshotFromRepository ?? await snapshotManager.GetLastSavedDependencyGraphAsync(snapshotOptions, ct);
                await renderer.RenderDiffViewsAndSaveToFiles(graph, compareGraph, renderOptions, ct);
            }
            else
                 await renderer.RenderViewsAndSaveToFiles(graph, renderOptions, ct);
         }
 
         await snapshotManager.SaveGraphAsync(graph, snapshotOptions, ct);
     }
 }
 