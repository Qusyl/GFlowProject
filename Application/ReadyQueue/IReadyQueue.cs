
using Domain.Nodes;

namespace Application.ReadyQueue
{
    public interface IReadyQueue
    {
         bool Empty { get;}
        ValueTask<NodeExecution> ReadAsync(CancellationToken cts = default);
        
        ValueTask WriteAsync(NodeExecution node, CancellationToken cts = default);
    }
}