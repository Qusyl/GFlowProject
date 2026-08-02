namespace Application.Runtime.Workflow;

public interface IWorkflowFactory
{
    WorkflowRuntime Create();
}