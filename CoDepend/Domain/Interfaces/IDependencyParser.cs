using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoDepend.Domain.Models.Records;

namespace CoDepend.Domain.Interfaces;

public interface IDependencyParser
{
    Task<IReadOnlyList<RelativePath>> ParseFileDependencies(string absPath, CancellationToken ct = default);
}