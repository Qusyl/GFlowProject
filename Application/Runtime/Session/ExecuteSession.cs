using Application.Context;
using Application.ReadyQueue;
using Application.Runtime.Workers;
using Application.Scheduler;

namespace Application.Runtime.Session;

public sealed class ExecuteSession {
    
    private readonly  IReadyQueue _queue;
    private readonly  NodeScheduler _scheduler;
    private readonly WorkerPool _workers;
    private readonly  WorkflowExecutionContext _context; 

    public ExecuteSession(IReadyQueue queue, NodeScheduler scheduler, WorkflowExecutionContext context)
    {
        
        _queue = queue;
        _scheduler = scheduler;
        _workers = WorkerPool.Create(32);
        _context = context;
        
    }

    public async  Task<RuntimeResult> StartAsync(CancellationToken token)
    {
        await _scheduler.InitializeAsync(token);

        while (!token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
            if(_queue.Empty && _workers.BusyWorkers == 0)
            {
                break;
            }
            var node = await _queue.ReadAsync(token);

            var context = new NodeExecutionContext(_context, node, token);

            var worker = await _workers.AcquireAsync(token);

            try
            {
        
                NodeResult result = await worker!.ProcessAsync(context);

                await _scheduler.OnNodeCompletedAsync(node.Node.Id, result);
            }
            finally
            {
                if (worker is not null)
                {
                    _workers.ReturnWorker(worker);
                }
            }
        }
        return RuntimeResult.Success;
    }
}

