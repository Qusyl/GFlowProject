namespace Domain.Nodes
{
    public sealed record NodeResult(string OutputPort, Dictionary<string, object?> Variables, Exception? InnerException);
}