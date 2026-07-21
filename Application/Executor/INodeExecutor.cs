using Domain.Nodes;
namespace Application.Executor
{
    public interface INodeExecutor
    {
        Task<NodeExecuteResult> ExecuteAsync(NodeExecutionContext context, CancellationToken cts = default);
    }
}