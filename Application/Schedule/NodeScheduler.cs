
using Application.ReadyQueue;
using Domain.Graph;
using Domain.Nodes;

namespace Application.Scheduler;

public class NodeScheduler
{
    private readonly IReadyQueue _readyQueue;
    private readonly ExecutionGraph _graph;
    public NodeScheduler(IReadyQueue queue, ExecutionGraph graph)
    {
        _readyQueue = queue;
        _graph = graph;
    
    }
    public async Task InitializeAsync( CancellationToken cts)
    {
        foreach(var (id, node) in _graph.Nodes)
        {
            var incoming = _graph.GetIncoming(id);
            if (incoming is null)
            {
                await _readyQueue.WriteAsync(node, cts);
            }
        }
    }
    public async Task OnNodeCompletedAsync(int nodeId, NodeResult result)
    {
        var edges = _graph.GetOutcoming(nodeId);
        if (!result.IsSuccess)
        {
            return;
        }
        if(edges is not null)
        {
               
             foreach(var edge in edges)
            {
                var outcomingPorts = result.Ports;
                if (outcomingPorts!.All(port => port.PortName != edge.FromPort))
                {
                    continue;
                }
                var nextNode = _graph.Nodes[edge.ToNode];

                await _readyQueue.WriteAsync(nextNode);

            }
        }
    }
}
