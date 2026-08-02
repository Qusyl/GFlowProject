using Application.Context;
using Application.Executors.Configurations;
using Domain.Nodes;

namespace Application.Executors.Trigger.Manual;

[NodeExecutor("ManualTrigger")]
public class TriggerManualExecutor : INodeExecutors
{
    public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
    {
        var amount = context.Node.Node.GetProperty<int>("Amount");
        context.WorkflowContext.SetVariable("Amount", new VariableValue(new VariableDefenition("Amount", Logic.Logics.ArgumentType.Integer), amount));
        return NodeResult.Success(context.Node.Descriptor.GetOutcomingPorts());
    }
}