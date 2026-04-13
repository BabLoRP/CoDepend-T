using CoDepend.Domain.Models.Records;

namespace CoDepend.Application;

public class ConfigManager(
    BaseOptions baseOptions,
    ParserOptions parserOptions,
    RenderOptions renderOptions,
    SnapshotOptions snapshotOptions)
{
    public BaseOptions GetBaseOptions() => baseOptions;
    public ParserOptions GetParserOptions() => parserOptions;
    public RenderOptions GetRenderOptions() => renderOptions;
    public SnapshotOptions GetSnapshotOptions() => snapshotOptions;
}
