using Domain.Nodes;

namespace Domain.Graph
{
    public sealed record Workflow(
     IReadOnlyCollection<Node> Nodes,
     IReadOnlyCollection<Edge> Edges);
     
}