
using Application.Executors;
using Domain.Nodes;
namespace Application.Runtime.Workers;

public interface IWorker
{
    Task<NodeResult> ProcessAsync(NodeExecution node, CancellationToken cts = default);
    
}
