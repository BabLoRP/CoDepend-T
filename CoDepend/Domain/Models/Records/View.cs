using System.Collections.Generic;

namespace CoDepend.Domain.Models.Records;

public sealed record View(
    string ViewName,
    IReadOnlyList<Package> Packages,
    IReadOnlyList<string> IgnorePackages
);
