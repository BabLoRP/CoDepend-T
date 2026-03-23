using System;
using CoDepend.Domain.Interfaces;
using CoDepend.Domain.Models.Enums;
using CoDepend.Domain.Models.Records;
using CoDepend.Infra.SnapshotManagers;

namespace CoDepend.Infra.Factories;

public static class SnapshotManagerFactory
{
    public static ISnapshotManager SelectSnapshotManager(SnapshotOptions o) => o.SnapshotManager switch
    {
        SnapshotManager.Git => new GitSnapshotManager(o.SnapshotDir, o.SnapshotFile),
        SnapshotManager.Local => new LocalSnapshotManager(o.SnapshotDir, o.SnapshotFile),
        _ => throw new ArgumentOutOfRangeException(nameof(o.SnapshotManager))
    };
}