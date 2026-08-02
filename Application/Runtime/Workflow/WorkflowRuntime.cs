using Application.Runtime.Session;
using Application.ReadyQueue;
using Application.Scheduler;
using Application.Context;
using Application.Executors.Configurations;
using Application.Runtime.Utillits;

namespace Application.Runtime.Workflow;

public class WorkflowRuntime
{
    private ExecuteSession _session;

    private readonly IExecutorResolver _registry;
    public WorkflowRuntime(IExecutorResolver registry)
    {
        _registry = registry;
    }

    public async Task<RuntimeResult> RunAsync(Workflow workflow,CancellationToken cts = default)
    {
        var workflowBuilder = new WorkflowBuilder();
        var graph = workflowBuilder.Build(workflow);
        var queue = new ReadyQueue.ReadyQueue(graph.Nodes.Count);

        var exceptionQueue = new WorkflowRuntimeErrorQueue();

        var sheduler = new NodeScheduler(queue, graph);
        
        var context = new WorkflowExecutionContext(cts);
        if (graph is not null)
        {
            _session = new ExecuteSession(queue, exceptionQueue, _registry, sheduler, context);
            await _session.StartAsync(cts);
        }
        else
        {
            await exceptionQueue.WriteAsync(new NullReferenceException("Graph is null"));
        }
        
        var exceptions = await exceptionQueue.ReadAllAsync(cts);

        return new RuntimeResult(exceptions);
            
    }
}
