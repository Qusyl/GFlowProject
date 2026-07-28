using Application.Executors.Configurations;
using Application.ReadyQueue;
using Domain.Nodes;

namespace Application.Runtime.Workers;

public class SessionWorker : IWorker
{
    private readonly ExecutorRegistry _registry;

    public SessionWorker(ExecutorRegistry registry)
    {
        _registry = registry;
       
    }
    public async Task<NodeResult> ProcessAsync(NodeExecution node, CancellationToken cts = default)
    {
        var executor = _registry.Resolve(node.Node.Type);

        if (executor is null)
        {
            return NodeResult.Failure(new NullReferenceException("executor is null"));
        }

        var result = await executor.ExecuteAsync(node, cts);

        return result;
    }
}