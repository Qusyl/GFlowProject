using Application.Context;
using Application.Exceptions;
using Application.Executors.Arguments;
using Application.Executors.Configurations;
using Application.Executors.Logic.Logics;

namespace Application.Executors.Logic;

[NodeExecutor("Comparer")]
public class LogicCompareExecutor : INodeExecutors
{
    private readonly LogicComparer _comparer;

    private readonly IArgumentResolver _resolver;
    public LogicCompareExecutor()
    {
        _comparer = new LogicComparer(); //скорее всего нужно сделать static class
        _resolver = new ArgumentResolver();
    }
    public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
    {
        var expression = context.Node.Node.GetProperty<LogicDefiniton>("Expression");
        System.Diagnostics.Debug.WriteLine($"Expression = null ? {expression is null}");
        if (expression is null)
        {
          
            return NodeResult.Failure(new NullNodePropertyReferenceException($"Can't get property {typeof(LogicDefiniton)} (comparer)"));
        }
        try
        {
            System.Diagnostics.Debug.WriteLine($"Начинаю работу с аргументами...");
            var leftArgument = _resolver.Resolve(expression.Left, context);

            var rightArgument = _resolver.Resolve(expression.Right, context);
            var compareResult = _comparer.Compare(leftArgument, expression.Operand, rightArgument);

            return compareResult is true
            ? NodeResult.Success([.. context.Node.Descriptor.GetOutcomingPorts()!.Where(p => p.PortName == "True")])
            : NodeResult.Success([.. context.Node.Descriptor.GetOutcomingPorts()!.Where(p => p.PortName == "False")]);
        }
        catch (Exception nullEx)
        {
            return NodeResult.Failure(nullEx);
        }
       
    }
}