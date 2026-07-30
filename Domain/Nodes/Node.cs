namespace Domain.Nodes
{
    public class Node
    {
        public int Id { get; init; }
        public string Type { get; init; }
        public IReadOnlyDictionary<string, object?> Properties { get; init; }

        public T? GetProperty<T>(string name)
        {
            if (Properties.TryGetValue(name, out var property))
            {
                return (T?)property;
            }

            return default;
        }
    }
}

