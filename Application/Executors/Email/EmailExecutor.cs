using Application.Executors.Configurations;
using Domain.Nodes;
namespace Application.Executors.Email;

[NodeExecutor("Email")]
public class EmailExecutor : INodeExecutors
{
    public async Task<NodeResult> ExecuteAsync(NodeExecution node, CancellationToken cts = default)
    {
        Console.WriteLine("Email movement");
        var ports = node.Descriptor.GetOutcomingPorts();
        return NodeResult.Success(ports);
        }
}