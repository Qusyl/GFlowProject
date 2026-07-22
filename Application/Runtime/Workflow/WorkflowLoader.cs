using System.Text.Json;

namespace Application.Runtime.Workflow;

public static class WorkflowLoader
{
    public static Workflow? LoadWorkflow(string json)
    {
        return  JsonSerializer.Deserialize<Workflow>(json);
    }
}
