using Application.Context;
using Application.Executors.Configurations;

namespace Application.Executors.Action.File;

[NodeExecutor("File")]
public class ActionFileExecutor : INodeExecutors
{
    public ActionFileExecutor(){}
    public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
    {
        System.Console.WriteLine("FIle operation...");
        await Task.Delay(2000, context.CancellationToken);
        return NodeResult.Success(context.Node.Descriptor.GetOutcomingPorts());
    }
}