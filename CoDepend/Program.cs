using System;
using System.IO;
using System.Threading.Tasks;
using CoDepend.Application;
using CoDepend.Infra;
using CoDepend.Infra.Factories;

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
            var resolvedPath = configPath.Length > 0 ? configPath : FindConfigFile("codepend.json");
            var configManager = await new LoadConfigUseCase(resolvedPath).RunAsync(diff, format);

            var snapshotManager = SnapshotManagerFactory.SelectSnapshotManager(configManager.GetSnapshotOptions());
            var parsers = DependencyParserFactory.SelectDependencyParser(configManager.GetParserOptions());
            var renderer = RendererFactory.SelectRenderer(configManager.GetRenderOptions());

            var useCase = new UpdateGraphUseCase(configManager, parsers, renderer, snapshotManager, diff);
            await useCase.RunAsync();

            Console.WriteLine($"Success! Diagrams available in: {configManager.GetRenderOptions().SaveLocation}");
            return "";
        }
        catch (Exception e)
        {
            Console.WriteLine($"EXCEPTION: {e.Message}\n{e.StackTrace}");
            return $"EXCEPTION: {e.Message}\n{e.StackTrace}";
        }
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
