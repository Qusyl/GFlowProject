using System.Threading.Channels;

using Domain.Nodes;


namespace Application.ReadyQueue
{
    public class ReadyQueue : IReadyQueue
    {
        private readonly Channel<NodeExecution> _queue;

        public bool Empty => !_queue.Reader.TryPeek(out var node);
        public ReadyQueue(int nodesPlanned)
        {
            var options = new BoundedChannelOptions(nodesPlanned)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<NodeExecution>(options);
        }
        public async ValueTask<NodeExecution?> ReadAsync(CancellationToken cts)
        {
            using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cts);
            timeout.CancelAfter(TimeSpan.FromMilliseconds(500));
            try
            {
                 if (await _queue.Reader.WaitToReadAsync(timeout.Token))
                {
                
                if (_queue.Reader.TryRead(out var node))
                {
                    return node;
                }
            }
            }
            catch(OperationCanceledException)
            {
                if (cts.IsCancellationRequested)
                {
                    throw;
                }
                return null;
            }

            return null;
        }

        public async ValueTask WriteAsync(NodeExecution node, CancellationToken cts)
        {
            if (await _queue.Writer.WaitToWriteAsync(cts))
            {
                await _queue.Writer.WriteAsync(node, cts);
            }
        }

    }
}