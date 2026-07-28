using Domain.Graph;
using Domain.Nodes;

namespace Application.Dto;

public sealed record WorkflowDto(IReadOnlyCollection<NodeExecution> Nodes, IReadOnlyCollection<Edge> Edges);
