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
        var graph = workflowBuilder.Build(workflow);
        var queue = new ReadyQueue.ReadyQueue(graph.Nodes.Count);

        var sheduler = new NodeScheduler(queue, graph);

        //сделать variables

        var context = new WorkflowExecutionContext(cts);
        if (graph is null)
        {
            return RuntimeResult.Failure(new InvalidOperationException("graph is null or empty"));
        }

        _session = new ExecuteSession(queue, sheduler, context);

        var result = await _session.StartAsync(cts);

        return result;
    }
}
