using Domain.Nodes;
namespace Application.Context;

/// <summary>
/// Создается для каждого Executor и хранит контекст их выполнения
/// </summary>
/// <param name="Node"></param>
/// <param name="Context"></param>
/// <param name="CancellationToken">
public sealed class NodeExecutionContext
{
    public WorkflowExecutionContext WorkflowContext { get; }

    public NodeExecution Node { get; }
    
    public CancellationToken CancellationToken { get; }

    public NodeExecutionContext(WorkflowExecutionContext context, NodeExecution node, CancellationToken token)
    {
        WorkflowContext = context;
        Node = node;
        CancellationToken = token;
    }

}
