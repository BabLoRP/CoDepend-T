using CoDepend.Domain;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;

namespace CoDependTests.Domain;

public class ConfigManagerTests
{
    private static BaseOptions MakeBaseOptions() =>
        new("/full/root", "/root", "TestProject");

    private static ParserOptions MakeParserOptions(BaseOptions b) =>
        new(b, [Language.CSharp], [".vs/"], [".cs"]);

    private static RenderOptions MakeRenderOptions(BaseOptions b) =>
        new(b, RenderFormat.PlantUML, [], "/save");

    private static SnapshotOptions MakeSnapshotOptions(BaseOptions b) =>
        new(b, SnapshotManager.Local, new GitInfo("", ""));

    [Fact]
    public void GetBaseOptions_WhenLoaded_ReturnsOptions()
    {
        var baseOpts = MakeBaseOptions();
        var cm = new ConfigManager(
            baseOpts,
            MakeParserOptions(baseOpts),
            MakeRenderOptions(baseOpts),
            MakeSnapshotOptions(baseOpts));

        Assert.Equal(baseOpts, cm.GetBaseOptions());
    }

    [Fact]
    public void GetParserOptions_WhenLoaded_ReturnsOptions()
    {
        var baseOpts = MakeBaseOptions();
        var parserOpts = MakeParserOptions(baseOpts);
        var cm = new ConfigManager(
            baseOpts, parserOpts,
            MakeRenderOptions(baseOpts),
            MakeSnapshotOptions(baseOpts));

        Assert.Equal(parserOpts, cm.GetParserOptions());
    }

    [Fact]
    public void GetRenderOptions_WhenLoaded_ReturnsOptions()
    {
        var baseOpts = MakeBaseOptions();
        var renderOpts = MakeRenderOptions(baseOpts);
        var cm = new ConfigManager(
            baseOpts,
            MakeParserOptions(baseOpts),
            renderOpts,
            MakeSnapshotOptions(baseOpts));

        Assert.Equal(renderOpts, cm.GetRenderOptions());
    }

    [Fact]
    public void GetSnapshotOptions_WhenLoaded_ReturnsOptions()
    {
        var baseOpts = MakeBaseOptions();
        var snapshotOpts = MakeSnapshotOptions(baseOpts);
        var cm = new ConfigManager(
            baseOpts,
            MakeParserOptions(baseOpts),
            MakeRenderOptions(baseOpts),
            snapshotOpts);

        Assert.Equal(snapshotOpts, cm.GetSnapshotOptions());
    }

    [Fact]
    public void GetBaseOptions_WhenNotLoaded_ThrowsInvalidOperationException()
    {
        var cm = new ConfigManager();
        Assert.Throws<InvalidOperationException>(() => cm.GetBaseOptions());
    }

    [Fact]
    public void GetParserOptions_WhenNotLoaded_ThrowsInvalidOperationException()
    {
        var cm = new ConfigManager();
        Assert.Throws<InvalidOperationException>(() => cm.GetParserOptions());
    }

    [Fact]
    public void GetRenderOptions_WhenNotLoaded_ThrowsInvalidOperationException()
    {
        var cm = new ConfigManager();
        Assert.Throws<InvalidOperationException>(() => cm.GetRenderOptions());
    }

    [Fact]
    public void GetSnapshotOptions_WhenNotLoaded_ThrowsInvalidOperationException()
    {
        var cm = new ConfigManager();
        Assert.Throws<InvalidOperationException>(() => cm.GetSnapshotOptions());
    }
}
