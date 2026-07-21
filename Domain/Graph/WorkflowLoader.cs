using System.Text.Json;

namespace Domain.Graph;

public static class WorkflowLoader
{
    public static Workflow? LoadWorkflow(string json)
    {
        return  JsonSerializer.Deserialize<Workflow>(json);
    }
}
