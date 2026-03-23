namespace CoDepend.Domain.Models.Records;

public sealed record Package(
    string Path,
    int Depth
);