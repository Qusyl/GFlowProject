
using Application.Executor.Configuration;


namespace Application.Executor
{
    [NodeExecutor("Email")]
    public class EmailExecutor : INodeExecutor
    {
        public Task<NodeResult> ExecuteAsync(NodeExecutionContext context, CancellationToken cts = default)
        {
            throw new NotImplementedException();
        }
    }
}