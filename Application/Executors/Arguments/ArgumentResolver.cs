using Application.Context;
using Application.Executors.Logic.Logics;

namespace Application.Executors.Arguments;

public class ArgumentResolver : IArgumentResolver
{
    public VariableValue Resolve(LogicArgument argument, NodeExecutionContext context)
    {
        if (argument is ConstantArgument constant)
        {
            return new VariableValue(new VariableDefenition("", constant.Type), constant.Value);
        }
        else if (argument is VariableArgument variable)
        {
            return context.WorkflowContext.GetVariable(variable.VariableName) ?? throw new NullReferenceException("Variable wasn't found or null");
        }

        throw new InvalidOperationException("Can't resolve argument");
    }
}