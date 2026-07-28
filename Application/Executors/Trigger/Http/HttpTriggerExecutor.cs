using Domain.Nodes;
using Application.Executors.Configurations;

namespace Application.Executors.Trigger.Email;

public class HttpTriggerExecutor : INodeExecutors
{
    public Task<NodeResult> ExecuteAsync(NodeExecution node, CancellationToken cts = default)
    {
        throw new NotImplementedException();
    }
}
