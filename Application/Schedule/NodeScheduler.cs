using Application.Context;
using Application.ReadyQueue;
using Domain.Nodes;
using Domain.Graph;


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
    public async Task StartAsync()
    {
        foreach(var (id, node) in _graph.Nodes)
        {
            if(_graph.GetIncoming(id)!.Count == 0)
            {
                await _readyQueue.WriteAsync(node);
            }
        }
    }
    public async Task OnNodeCompletedAsync(int nodeId, NodeResult result)
    {
        var edges = _graph.GetOutcoming(nodeId);
        if(edges is not null)
        {
             foreach(var edge in edges)
            {
                if (edge.FromPort != result.OutputPort)
                {
                    continue;
                }
               await _readyQueue.WriteAsync(_graph.Nodes[nodeId]);
            }
        }
       
    }
}
