using Application.Context;

namespace Application.Executors.Configurations;

public interface INodeExecutors
{
    Task<NodeResult> ExecuteAsync(NodeExecutionContext context);
}
