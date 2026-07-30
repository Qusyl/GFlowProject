using Application.Context;
using Application.Exceptions;
using Application.Executors.Logic.Logics;


namespace Application.Executors.Configurations;
public class LogicComparer
{
    public bool Compare(VariableValue left, LogicOperand operand, VariableValue right)
    {
        if (!IsComparable(left, right))
        {
            throw new NotEqualTypeException($"Comparer can't compare {left.Defenition.Type} and {right.Defenition.Type}");
        }

        var res = Comparer<object>.Default.Compare(left.Value, right.Value);

        return (res, operand) switch
        {
            (0, LogicOperand.EqualTo) => true,
            (0, LogicOperand.LessThanOrEqual) => true,
            (0, LogicOperand.GraterThanOrEqual) => true,
            (1, LogicOperand.GreaterThan) => true,
            (1, LogicOperand.GraterThanOrEqual) => true,
            (-1, LogicOperand.LessThan) => true,
            (-1, LogicOperand.LessThanOrEqual) => true,
            _ => false
        };
    }
    
    private bool IsComparable(VariableValue first, VariableValue second)
    {
        return first.Defenition.Type == second.Defenition.Type;
    }
}