using System;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Domain;

public sealed class ConfigManager : IConfigManager
{
    private readonly BaseOptions? _baseOptions;
    private readonly ParserOptions? _parserOptions;
    private readonly RenderOptions? _renderOptions;
    private readonly SnapshotOptions? _snapshotOptions;

    public ConfigManager() { }

    public ConfigManager(
        BaseOptions baseOptions,
        ParserOptions parserOptions,
        RenderOptions renderOptions,
        SnapshotOptions snapshotOptions)
    {
        _baseOptions = baseOptions;
        _parserOptions = parserOptions;
        _renderOptions = renderOptions;
        _snapshotOptions = snapshotOptions;
    }

    public BaseOptions GetBaseOptions() =>
        _baseOptions ?? throw new InvalidOperationException("Config has not been loaded. Call LoadAsync first.");

    public ParserOptions GetParserOptions() =>
        _parserOptions ?? throw new InvalidOperationException("Config has not been loaded. Call LoadAsync first.");

    public RenderOptions GetRenderOptions() =>
        _renderOptions ?? throw new InvalidOperationException("Config has not been loaded. Call LoadAsync first.");

    public SnapshotOptions GetSnapshotOptions() =>
        _snapshotOptions ?? throw new InvalidOperationException("Config has not been loaded. Call LoadAsync first.");
}
