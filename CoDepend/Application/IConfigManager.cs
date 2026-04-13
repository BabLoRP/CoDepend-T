using CoDepend.Domain.Models.Records;

namespace CoDepend.Application;

public interface IConfigManager
{
    BaseOptions GetBaseOptions();
    ParserOptions GetParserOptions();
    RenderOptions GetRenderOptions();
    SnapshotOptions GetSnapshotOptions();
}
