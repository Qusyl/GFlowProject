using Application.Context;
using Application.Executors.Configurations;
using Domain.Nodes;

namespace Application.Executors.Trigger.Manual;

[NodeExecutor("ManualTrigger")]
public class TriggerManualExecutor : INodeExecutors
{
    public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
    {
        return NodeResult.Success(context.Node.Descriptor.GetOutcomingPorts());
    }
}