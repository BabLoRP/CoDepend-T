
using System.Collections.Generic;

namespace CoDepend.Domain.Models.Records;

public sealed record ProjectChanges(
    IReadOnlyDictionary<RelativePath, IReadOnlyList<RelativePath>> ChangedFilesByDirectory,
    IReadOnlyList<RelativePath> DeletedFiles,
    IReadOnlyList<RelativePath> DeletedDirectories
);