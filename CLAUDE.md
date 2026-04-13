# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet build ./CoDepend          # build
dotnet test ./CoDepend           # run all tests
dotnet run --project ./CoDepend  # run the CLI (reads codepend.json from repo root)
```

Run a single test by name:
```bash
dotnet test ./CoDepend --filter "FullyQualifiedName~TestMethodName"
```

## Architecture

The solution has two projects: `CoDepend` (the tool) and `CoDependTests` (xUnit tests).

The tool follows clean architecture with a strict inward dependency rule:

```
Program.cs → Infra → Application → Domain
```

- **Domain** (`CoDepend/Domain/`): core models, interfaces (`IDependencyParser`, `ISnapshotManager`), `DependencyGraphSerializer` (MessagePack binary serialization), `RendererBase`.
- **Application** (`CoDepend/Application/`): `UpdateGraphUseCase` (main orchestrator), `DependencyGraphBuilder` (parallel parse → graph), `ChangeDetector` (diff since last snapshot), `ILogger`, `ConfigManager` (data holder for the four options structs).
- **Infra** (`CoDepend/Infra/`): `Logger`, `LoadConfigUseCase` (reads `codepend.json` and constructs `ConfigManager`), language parsers (C#/Java/Kotlin/Go), renderers (PlantUML/JSON/None), snapshot managers (Local/Git), factories that wire them up from config.
- **Program.cs**: CLI entry point; runs `LoadConfigUseCase`, instantiates `Logger`, calls factories with options from `ConfigManager`, builds and runs `UpdateGraphUseCase`.

### Key data flow

1. `LoadConfigUseCase.RunAsync()` reads `codepend.json` and returns a `ConfigManager` holding the four options structs (`BaseOptions`, `ParserOptions`, `RenderOptions`, `SnapshotOptions`).
2. `SnapshotManagerFactory`, `DependencyParserFactory`, `RendererFactory` select implementations from options queried via `ConfigManager.Get*Options()`.
3. `UpdateGraphUseCase.RunAsync()` orchestrates: load last snapshot → detect changes → build graph → render → save snapshot.
4. `DependencyGraphBuilder` processes file changes in parallel using all registered parsers.
5. Snapshot is serialized with MessagePack + Lz4 compression via `DependencyGraphSerializer`.

### Dependency policy (enforced by ArchLens)

- Domain: no external dependencies.
- Application: depends only on Domain.
- Infra: depends on Domain and Application.
- `Program.cs`: depends on everything.

Interfaces that cross the Application→Infra boundary live in Domain (`ISnapshotManager`, `IDependencyParser`) or Application (`ILogger`), implemented in Infra.

## Conventions

- Primary constructors throughout; records for immutable value types.
- File-scoped namespaces.
- `RelativePath` wraps all paths — use `RelativePath.File()` / `RelativePath.Directory()` factory methods.
- Cancellation tokens are threaded through all async methods.
