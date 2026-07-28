using Application.Executors.Configurations;
using Domain.Nodes;

namespace Application.Executors.Trigger.Manual;

public class TriggerManualExecutor : INodeExecutors
{
    public async Task<NodeResult> ExecuteAsync(NodeExecution node, CancellationToken cts = default)
    {
        return NodeResult.Success(node.Descriptor.GetOutcomingPorts());
    }
}