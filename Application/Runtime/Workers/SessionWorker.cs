using Application.Context;
using Application.Executors.Configurations;


namespace Application.Runtime.Workers;

public class SessionWorker : IWorker
{
    private readonly ExecutorRegistry _registry;

    public SessionWorker(ExecutorRegistry registry)
    {
        _registry = registry;
       
    }
    public async Task<NodeResult> ProcessAsync(NodeExecutionContext context)
    {
        var executor = _registry.Resolve(context.Node.Node.Type);

        if (executor is null)
        {
            return NodeResult.Failure(new NullReferenceException("executor is null"));
        }

        var result = await executor.ExecuteAsync(context);

        return result;
    }
}