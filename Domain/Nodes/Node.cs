using System.Text.Json;

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
                if(property is JsonElement element)
                {
                    return JsonSerializer.Deserialize<T>(element);
                }
                else if(property is T readyType)
                {
                    return readyType;
                }

                try
                {
                    return (T?)Convert.ChangeType(property, typeof(T));
                }
                catch
                {
                    //Сделать перехват
                    return default;
                }
            }
            

            return default;
        }
    }
}

