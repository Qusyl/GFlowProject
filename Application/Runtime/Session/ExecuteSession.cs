using Application.Context;
using Application.Executors;
using Application.ReadyQueue;

using Application.Scheduler;
using Application.Workers;
using Domain.Graph;

namespace Application.Runtime.Session;

public sealed class ExecuteSession {
    public ExecutionGraph Graph;
    public IReadyQueue Queue;
    public NodeScheduler Scheduler;
    private readonly WorkerPool _workers;
    public WorkflowExecutionContext Context;

    public ExecuteSession(ExecutionGraph graph, IReadyQueue queue, NodeScheduler scheduler, WorkflowExecutionContext context)
    {
        Graph = graph;
        Queue = queue;
        Scheduler = scheduler;
        _workers = WorkerPool.Create(32);
        Context = context;
    }

    public async  Task<RuntimeResult> StartAsync(CancellationToken token)
    {
        await Scheduler.StartAsync(token);

        while (!token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
            var worker = _workers.Get();
            var node = await Queue.ReadAsync(token);

            try
            {
                NodeResult result = await worker!.ProcessAsync(node, token);

                await Scheduler.OnNodeCompletedAsync(node.Node.Id, result);
            }
            finally
            {
                if (worker is not null)
                {
                    _workers.Return(worker);
                }
            }

            if (Scheduler.IsCompleted)
            {
                break;
            }
        }
        return RuntimeResult.Success;
    }
}

