using System;
using CoDepend.Domain;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra.Renderers;

namespace CoDepend.Infra.Factories;

public static class RendererFactory
{
    public static RendererBase SelectRenderer(RenderOptions options) => options.Format switch
    {
        RenderFormat.None => new NoneRenderer(),
        RenderFormat.Json => new JsonRenderer(),
        RenderFormat.PlantUML => new PlantUMLRenderer(),
        _ => throw new ArgumentOutOfRangeException(nameof(options))
    };
}