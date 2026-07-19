using Domain.Nodes;
namespace Application.ReadyQueue
{
    public interface IReadyQueue
    {
        ValueTask<NodeExecution> ReadAsync(CancellationToken cts);

        ValueTask WriteAsync(NodeExecution node, CancellationToken cts);
    }
}