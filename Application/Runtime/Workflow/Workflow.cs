using Domain.Graph;
using Domain.Nodes;

namespace Application.Runtime.Workflow
{
    public sealed record Workflow(
     IReadOnlyCollection<NodeExecution> Nodes,
     IReadOnlyCollection<Edge> Edges);
}