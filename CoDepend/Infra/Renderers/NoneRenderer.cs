using System;
using CoDepend.Domain;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Infra.Renderers;

public sealed class NoneRenderer : RendererBase
{
    public override string FileExtension => "";

    protected override string Render(RenderGraph graph, View view, RenderOptions options)
    {
        Console.WriteLine("Info: Renderer is none - no output will be rendered.");
        return "";
    }
}