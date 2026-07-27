
using Application.Executors;
using Domain.Nodes;

namespace Application.ReadyQueue
{
    public interface IReadyQueue
    {
        ValueTask<NodeExecution> ReadAsync(CancellationToken cts = default);

        ValueTask WriteAsync(NodeExecution node, CancellationToken cts = default);
    }
}