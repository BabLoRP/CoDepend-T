using CoDepend.Application;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Infra;

public class ConfigManager(
    BaseOptions baseOptions,
    ParserOptions parserOptions,
    RenderOptions renderOptions,
    SnapshotOptions snapshotOptions) : IConfigManager
{
    public BaseOptions GetBaseOptions() => baseOptions;
    public ParserOptions GetParserOptions() => parserOptions;
    public RenderOptions GetRenderOptions() => renderOptions;
    public SnapshotOptions GetSnapshotOptions() => snapshotOptions;
}
