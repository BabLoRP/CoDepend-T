using System;
using System.Collections.Generic;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra.Parsers;

namespace CoDepend.Infra.Factories;

public static class DependencyParserFactory
{
    public static IReadOnlyList<IDependencyParser> SelectDependencyParser(ParserOptions o)
    {
        List<IDependencyParser> parsers = [];

        foreach (var lang in o.Languages)
        {
            IDependencyParser parser = lang switch
            {
                Language.CSharp => new CsharpSyntaxWalkerParser(o),
                Language.Go => new GoDependencyParser(o),
                Language.Kotlin => new KotlinDependencyParser(o),
                Language.Java => new JavaDependencyParser(o),
                _ => throw new NotSupportedException(nameof(lang))
            };
            parsers.Add(parser);
        }
        return parsers;
    }
}