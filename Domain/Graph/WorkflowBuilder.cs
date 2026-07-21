using Domain.Nodes;
using Domain.Ports;

namespace Domain.Graph;

public static class WorkflowBuilder
{

    public static ExecutionGraph Build(Workflow workflow)
    {
        var Nodes = BuildNodes(workflow);
        var IncomingEdges = new Dictionary<int, List<Edge>>();

        var OutcomingEdges = new Dictionary<int, List<Edge>>();

        foreach(var edge in workflow.Edges)
        {
            IncomingEdges[edge.ToNode].Add(edge);
            OutcomingEdges[edge.FromNode].Add(edge);
        }

        return new ExecutionGraph(Nodes, IncomingEdges, OutcomingEdges);
    }
    private static Dictionary<int, Node> BuildNodes(Workflow workflow)
    {
        var nodes = new Dictionary<int, Node>(workflow.Nodes.Count);

        foreach (var node in workflow.Nodes)
        {
            if (node is not null)
            {
                nodes.TryAdd(node.Id, node);
            }
        }
        return nodes;
    }

}
