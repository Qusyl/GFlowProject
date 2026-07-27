
using Domain.Graph;
using Domain.Nodes;

namespace Application.Runtime.Workflow;

public class WorkflowBuilder
{
    public ExecutionGraph Build(Workflow workflow)
    {
        var Nodes = BuildNodes(workflow);
        var IncomingEdges = new Dictionary<int, List<Edge>>();

        var OutcomingEdges = new Dictionary<int, List<Edge>>();

        foreach (var edge in workflow.Edges)
        {
            IncomingEdges[edge.ToNode].Add(edge);
            OutcomingEdges[edge.FromNode].Add(edge);
        }

        return new ExecutionGraph(Nodes, IncomingEdges, OutcomingEdges);
    }
    private Dictionary<int, NodeExecution> BuildNodes(Workflow workflow)
    {
        var nodes = new Dictionary<int, NodeExecution>(workflow.Nodes.Count);

        foreach (var node in workflow.Nodes)
        {
            if (node is not null)
            {
                nodes.TryAdd(node.Node.Id, node);
            }
        }
        return nodes;
    }
}