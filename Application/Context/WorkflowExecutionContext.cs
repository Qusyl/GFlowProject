using Microsoft.Extensions.Logging;

namespace Application.Context;

public sealed class WorkflowExecutionContext
{

    public Guid ExecutionId { get; init; }
    public CancellationToken CancellationToken { get; init; }
    public Dictionary<string, VariableValue> Variables { get; init; }
    
    public DateTimeOffset StartedTime { get; init; }

    public WorkflowExecutionContext(CancellationToken token)
    {
        Variables = new Dictionary<string, VariableValue>();
        ExecutionId = Guid.NewGuid();
        StartedTime = DateTimeOffset.UtcNow;
        CancellationToken = token;

    }

    public void SetVariable(string name, VariableValue value)
    {
        Variables.Add(name, value);
    }
    public VariableValue? GetVariable(string name)
    {
        if (Variables.TryGetValue(name, out var variable))
        {
            return variable;
        }

        return default;
    }  

}
