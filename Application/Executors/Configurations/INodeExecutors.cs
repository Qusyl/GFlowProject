using Application.Executors;
using Domain.Nodes;
namespace Application.Executors.Configurations;

public interface INodeExecutors
{
    Task<NodeResult> ExecuteAsync(NodeExecution node, CancellationToken cts = default);
}
