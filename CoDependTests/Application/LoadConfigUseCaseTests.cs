using System.Threading;
using System.Threading.Tasks;
using CoDepend.Application;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Domain.Interfaces;

namespace CoDependTests.Application;

public class LoadConfigUseCaseTests
{
    private sealed class FakeConfigLoader : IConfigLoader
    {
        public (BaseOptions, ParserOptions, RenderOptions, SnapshotOptions) Result { get; set; }

        public Task<(BaseOptions, ParserOptions, RenderOptions, SnapshotOptions)> LoadAsync(
            string path, bool diff = false, string format = "puml", CancellationToken ct = default)
        {
            return Task.FromResult(Result);
        }
    }

    private static (BaseOptions, ParserOptions, RenderOptions, SnapshotOptions) MakeOptions()
    {
        var b = new BaseOptions("/full/root", "/root", "TestProject");
        var p = new ParserOptions(b, [Language.CSharp], [], [".cs"]);
        var r = new RenderOptions(b, RenderFormat.PlantUML, [], "/save");
        var s = new SnapshotOptions(b, SnapshotManager.Local, new GitInfo("", ""));
        return (b, p, r, s);
    }

    [Fact]
    public async Task RunAsync_ReturnsConfigManagerWithCorrectOptions()
    {
        var options = MakeOptions();
        var loader = new FakeConfigLoader { Result = options };
        var useCase = new LoadConfigUseCase(loader);

        var configManager = await useCase.RunAsync("some/path.json", diff: false, format: "puml");

        Assert.Equal(options.Item1, configManager.GetBaseOptions());
        Assert.Equal(options.Item2, configManager.GetParserOptions());
        Assert.Equal(options.Item3, configManager.GetRenderOptions());
        Assert.Equal(options.Item4, configManager.GetSnapshotOptions());
    }
}
