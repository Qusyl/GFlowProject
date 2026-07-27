using Application.Executors;
using Domain.Nodes;
namespace Application.Context;

public sealed record NodeExecutionContext(NodeExecution Node, ExecutionContext Context);
