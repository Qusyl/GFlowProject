namespace Domain.Nodes
{
    public sealed record NodeResult(int OutputPort, Dictionary<string, object?> Variables);
}