


using Application.Executors.Configurations;
using Application.Runtime.Workers;
using Domain.Nodes;

namespace Application.Workers;

public class ExecutionWorker : IWorker
{
    private readonly ExecutorRegistry _registry;

    public ExecutionWorker() { _registry = new ExecutorRegistry(); }

    public async Task<NodeResult> ProcessAsync(NodeExecution node, CancellationToken cts = default)
    {
        var executor = _registry.Resolve(node.Node.Type) ?? throw new InvalidOperationException("Invalid Executor type (is null or empty)");
        var result = await executor.ExecuteAsync(node, cts);

        return result;
    }
}
