using Application.Executors.Configuration;
using Application.Context;
using Domain.Nodes;
namespace Application.Runtime.Workers;

public class SessionWorker : IWorker
{
    private readonly ExecutorRegistry _registry;

    public SessionWorker( ExecutorRegistry registry)
    {
        _registry = registry;
       
       
    }
    public async Task<NodeResult> ProcessAsync(NodeExecutionContext context, CancellationToken cts = default)
    {
        var executor = _registry.Resolve(context.Node.Node.Type) ?? throw new InvalidOperationException("Invalid Executor type (is null or empty)");
        
        return await executor.ExecuteAsync(context, cts);
    }
}
