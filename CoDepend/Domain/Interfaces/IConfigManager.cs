using CoDepend.Domain.Models.Records;

namespace CoDepend.Domain.Interfaces;

public interface IConfigManager
{
    BaseOptions GetBaseOptions();
    ParserOptions GetParserOptions();
    RenderOptions GetRenderOptions();
    SnapshotOptions GetSnapshotOptions();
}
