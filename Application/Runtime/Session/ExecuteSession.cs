using Application.Context;
using Application.Executors.Configurations;
using Application.Queues.Exception;
using Application.ReadyQueue;
using Application.Runtime.Workers;
using Application.Scheduler;


namespace Application.Runtime.Session;

public sealed class ExecuteSession {

    private readonly IReadyQueue _queue;
    private readonly IExceptionQueue _exceptions;
    private readonly  NodeScheduler _scheduler;
    private readonly WorkerPool _workers;
    private readonly  WorkflowExecutionContext _context; 

    public ExecuteSession(IReadyQueue readyQueue,IExceptionQueue exceptionQueue,IExecutorResolver registry ,NodeScheduler scheduler, WorkflowExecutionContext context)
    {

        _queue = readyQueue;
        _exceptions = exceptionQueue;
        _scheduler = scheduler;
        _workers = WorkerPool.Create(32, registry);
        _context = context;
        
    }

    public async  Task StartAsync(CancellationToken token)
    {
        await _scheduler.InitializeAsync(token);

        while (!token.IsCancellationRequested)
        {
            token.ThrowIfCancellationRequested();
            var node = await _queue.ReadAsync(token);
            if(node is null)
            {
                if (_queue.Empty && _workers.IsIdle())
                {
                    break;
                }
                else if (_queue.Empty && _workers.BusyWorkers > 0)
                {
                    await Task.Delay(300, token);
                    continue;
                }
            
                continue;
            }
       
    
            var context = new NodeExecutionContext(_context, node, token);

            var worker = await _workers.AcquireAsync(token);

            try
            {

                NodeResult result = await worker!.ProcessAsync(context);
                if (!result.IsSuccess)
                {
                    await _exceptions.WriteAsync(result.Error!);
                }

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
    }
}

