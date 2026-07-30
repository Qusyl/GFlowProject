using Application.Context;
using Application.Executors.Logic.Logics;

namespace Application.Executors.Arguments;

public interface IArgumentResolver
{
    public VariableValue Resolve(LogicArgument argument, NodeExecutionContext context);
}