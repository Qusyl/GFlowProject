using Application.Runtime.Session;
using Application.ReadyQueue;
using Application.Scheduler;
using Application.Context;
namespace Application.Runtime.Workflow;

public class WorkflowRuntime
{
    private ExecuteSession _session;

    public async Task<RuntimeResult> RunAsync(Workflow workflow,CancellationToken cts = default)
    {
        var workflowBuilder = new WorkflowBuilder();

        var queue = new ReadyQueue.ReadyQueue();

        var graph = workflowBuilder.Build(workflow);

        var sheduler = new NodeScheduler(queue, graph);
        Dictionary<string, object> variables = new()
        {
            ["Created"] = DateTimeOffset.UtcNow
            //Пока что так
        };
        var context = new WorkflowExecutionContext(variables);
        if (graph is null)
        {
            return RuntimeResult.Failure(new InvalidOperationException("graph is null or empty"));
        }

        _session = new ExecuteSession(graph, queue, sheduler, context);

        var result = await _session.StartAsync(cts);

        return result;
    }
}
