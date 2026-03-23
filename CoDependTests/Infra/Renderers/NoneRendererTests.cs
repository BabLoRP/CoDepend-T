using CoDepend.Domain.Models;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra.Renderers;
using CoDependTests.Utils;

namespace CoDependTests.Infra.Renderers;

public sealed class NoneRendererTests : IDisposable
{
    private readonly TestFileSystem _fs = new();
    private readonly NoneRenderer _renderer = new();

    public void Dispose() => _fs.Dispose();

    private RenderOptions Opts(
        string viewName = "testView",
        IReadOnlyList<Package>? packages = null,
        IReadOnlyList<string>? ignore = null) => new(
        BaseOptions: new(
            ProjectRoot: _fs.Root,
            ProjectName: "CoDepend",
            FullRootPath: _fs.Root),
        Format: RenderFormat.None,
        Views: [new View(viewName, packages ?? [], ignore ?? [])],
        SaveLocation: $"{_fs.Root}/diagrams");

    private ProjectDependencyGraph DefaultGraph() =>
        TestDependencyGraph.MakeDependencyGraph(_fs.Root);

    private string Render(ProjectDependencyGraph graph, RenderOptions opts) =>
        _renderer.RenderView(graph, opts.Views[0], opts);

    [Fact]
    public void Render_ReturnsEmptyString()
    {
        var result = Render(DefaultGraph(), Opts());
        Assert.Empty(result);
    }
}
