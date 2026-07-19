using Domain.Nodes;
namespace Application.Executor
{
    public interface INodeExecutor
    {
        public string Type { get; }

        Task<NodeExecuteResult> ExecuteAsync(NodeExecutionContext context, CancellationToken cts = default);
    }
}