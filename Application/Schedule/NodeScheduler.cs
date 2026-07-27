
using Application.ReadyQueue;
using Domain.Graph;

namespace Application.Scheduler;

public class NodeScheduler
{
    private readonly IReadyQueue _readyQueue;
    private readonly ExecutionGraph _graph;

    private bool _isCompleted;

    public bool IsCompleted => _isCompleted;
    public NodeScheduler(IReadyQueue queue, ExecutionGraph graph)
    {
        _readyQueue = queue;
        _graph = graph;
        _isCompleted = false;
    }
    public async Task StartAsync( CancellationToken cts)
    {
        foreach(var (id, node) in _graph.Nodes)
        {
            if(_graph.GetIncoming(id)!.Count == 0)
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
            //todo сделать перехватчик + обработчик событий
        }
        if(edges is not null)
        {
             foreach(var edge in edges)
            {
                var outcomingPorts = result.Ports;
                if(outcomingPorts?.Any(port => port.PortName == edge.FromPort) is not false)
                {
                    continue;
                }
               await _readyQueue.WriteAsync(_graph.Nodes[nodeId]);
            }
        }
    }
}
