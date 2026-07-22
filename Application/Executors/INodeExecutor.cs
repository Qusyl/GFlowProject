using Domain.Nodes;
namespace Application.Executor
{
    public interface INodeExecutor
    {
        Task<NodeResult> ExecuteAsync(NodeExecutionContext context, CancellationToken cts = default);
    }
}