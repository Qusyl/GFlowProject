using Application.Executors.Configurations;

namespace Application.Runtime.Workflow;

public class WorkflowFactory : IWorkflowFactory
{
    private readonly IExecutorResolver _registry;
    public WorkflowFactory(IExecutorResolver registry)
    {
        _registry = registry;
    }
    public WorkflowRuntime Create()
    {
        return new WorkflowRuntime(_registry);
    }
}