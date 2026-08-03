using System.Threading.Channels;
using Application.Queues.Exception;
using Application.ReadyQueue;
using Domain.Nodes;

namespace Application.Runtime.Utillits;

public sealed class WorkflowRuntimeErrorQueue : IExceptionQueue
{
    private Channel<Exception> _queue;

    public WorkflowRuntimeErrorQueue()
    {
        //протестировать, если нужно заменить на Bounded 
        _queue = Channel.CreateUnbounded<Exception>(); 
    }
    public bool Empty => !_queue.Reader.TryPeek(out var exception);

    public async Task<IReadOnlyCollection<Exception?>> ReadAllAsync(CancellationToken cts = default)
    {
        var list = new List<Exception>();

        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cts);
        timeout.CancelAfter(TimeSpan.FromMilliseconds(500));
        try
        {
             await foreach (var item in _queue.Reader.ReadAllAsync(timeout.Token))
        {
            list.Add(item);
        }
        return list;
        }catch(OperationCanceledException ex)
        {
            if (cts.IsCancellationRequested)
            {
                throw;
            }
            return null;
        }
       
    }

    public async ValueTask<Exception?> ReadAsync(CancellationToken cts = default)
    {
        return await _queue.Reader.ReadAsync(cts);
    }

    public async ValueTask WriteAsync(Exception exception, CancellationToken cts = default)
    {
        await _queue.Writer.WriteAsync(exception);
    }
}