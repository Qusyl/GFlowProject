using System.Threading.Channels;
using Domain.Nodes;

namespace Application.ReadyQueue
{
    public class ReadyQueue : IReadyQueue
    {
        private readonly Channel<ExecutionNode> _queue;

        public ReadyQueue()
        {
            var options = new BoundedChannelOptions(capacity: 100)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            _queue = Channel.CreateBounded<ExecutionNode>(options);
        }
        public async ValueTask<ExecutionNode> ReadAsync(CancellationToken cts)
        {
            if (await _queue.Reader.WaitToReadAsync(cts))
            {
                if (_queue.Reader.TryRead(out var node))
                {
                    return node;
                }
            }
            throw new InvalidOperationException("Канал был закрыт или доступ к нему заблокирован");
        }

        public async ValueTask WriteAsync(ExecutionNode node, CancellationToken cts)
        {
            if (await _queue.Writer.WaitToWriteAsync(cts))
            {
                await _queue.Writer.WriteAsync(node, cts);
            }
        }
    }
}