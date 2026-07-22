using Microsoft.Extensions.Logging;

namespace Application.Context;

public sealed class WorkflowExecutionContext
{
    public Dictionary<string, object> Variables { get; init; }
    
    public Guid WorkflowId { get; init; }

    public DateTimeOffset CreatedTime { get; init; }

    public ILogger<WorkflowExecutionContext> Logger { get; init; }

    public WorkflowExecutionContext(Dictionary<string, object> variables, ILogger<WorkflowExecutionContext> logger)
    {
        Variables = variables;
        WorkflowId = Guid.NewGuid();
        CreatedTime = DateTimeOffset.UtcNow;
        Logger = logger;
    }
}
