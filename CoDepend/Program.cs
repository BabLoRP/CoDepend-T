using System;
using System.IO;
using System.Threading.Tasks;
using CoDepend.Application;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra;
using CoDepend.Infra.Factories;
using Microsoft.Extensions.DependencyInjection;


#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace CoDepend.CLI;
#pragma warning restore IDE0130

public class Program
{
    public static async Task Main(string[] args)
    {
        var relPath = args.Length == 0 ? "../codepend.json" : args[0].Trim();

        var fileAbsPath = File.Exists(relPath)
                        ? new FileInfo(relPath).FullName
                        : "";

        var format = args.Length < 2 ? "puml" : args[1].Trim();
        var diff = args.Length >= 3 && args[2].Trim() == "diff";

        await CLI(fileAbsPath, format, diff);
    }

    public static string CLISync(string configPath, string format = "puml", bool diff = false)
    {
        return CLI(configPath, format, diff).GetAwaiter().GetResult();
    }

    public static async Task<string> CLI(string configPath, string format = "puml", bool diff = false)
    {
        try
        {
            var services = new ServiceCollection();

        var (baseOptions, parserOptions, renderOptions, snapshotOptions) =
            await LoadOptions(configPath, diff, format);

            // register options
            services.AddSingleton(baseOptions);
            services.AddSingleton(parserOptions);
            services.AddSingleton(renderOptions);
            services.AddSingleton(snapshotOptions);

            // factories (replace later with proper DI services if you want)
            var snapshotManager = SnapshotManagerFactory.SelectSnapshotManager(snapshotOptions);
            var parsers = DependencyParserFactory.SelectDependencyParser(parserOptions);
            var renderer = RendererFactory.SelectRenderer(renderOptions);

            services.AddSingleton(snapshotManager);
            services.AddSingleton(parsers);
            services.AddSingleton(renderer);

            // DI of logger
            services.AddSingleton<ILogger, Logger>();

            // application layer
            services.AddSingleton<UpdateGraphUseCase>();

            var provider = services.BuildServiceProvider();

            var useCase = provider.GetRequiredService<UpdateGraphUseCase>();
            await useCase.RunAsync();

            Console.WriteLine($"Success! Diagrams available in: {renderOptions.SaveLocation}");
            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine($"EXCEPTION: {e.Message}\n{e.StackTrace}");
            return $"EXCEPTION: {e.Message}\n{e.StackTrace}";
        }
    }

    private static async Task<(BaseOptions, ParserOptions, RenderOptions, SnapshotOptions)> LoadOptions(string configDir, bool diff = false, string format = "puml")
    {
        var resolvedPath = configDir.Length > 0 ? configDir : FindConfigFile("codepend.json");
        var configManager = new ConfigManager(resolvedPath);
        return await configManager.LoadAsync(diff, format);
    }

    private static string FindConfigFile(string fileName)
    {
        var dir = AppContext.BaseDirectory;

        while (!string.IsNullOrEmpty(dir))
        {
            var candidate = Path.Combine(dir, fileName);
            if (File.Exists(candidate))
                return candidate;

            dir = Directory.GetParent(dir)?.FullName;
        }

        throw new FileNotFoundException($"Could not find '{fileName}' starting from '{AppContext.BaseDirectory}'.");
    }
}
