
using Domain.Nodes;
using Application.Executor.Configuration;

namespace Application.Executor
{
    [NodeExecutor("Email")]
    public class EmailExecutor : INodeExecutor
    {
        public Task<NodeExecuteResult> ExecuteAsync(NodeExecutionContext context, CancellationToken cts = default)
        {
            throw new NotImplementedException();
        }
    }
}