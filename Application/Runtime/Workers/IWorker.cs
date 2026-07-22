using Domain.Nodes;
using Application.Context;
namespace Application.Runtime.Workers;

public interface IWorker
{
    Task<NodeResult> ProcessAsync(NodeExecutionContext node, CancellationToken cts = default);
}
