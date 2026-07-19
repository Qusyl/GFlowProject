namespace Domain.Nodes
{
    public record Node(
        int Id,
        string Type,
        IReadOnlyDictionary<string, object?> Properties);
   
}

