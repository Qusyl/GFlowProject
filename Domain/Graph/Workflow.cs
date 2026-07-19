using Domain.Nodes;

namespace Domain.Graph
{
    public sealed record Workflow(
     IReadOnlyCollection<Edge> Edges,
     IReadOnlyCollection<NodeDescriptor> Nodes);
}