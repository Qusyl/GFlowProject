using Application.Executors;
using Domain.Nodes;
namespace Application.Executors;

public interface INodeExecutors
{
    Task<NodeResult> ExecuteAsync(NodeExecution node, CancellationToken cts = default);
}
