using System.Text.Json;

namespace Domain.Nodes
{
    public class Node
    {
        private static int _idCouter = 1;
        public int Id { get;}
        public string Type { get; init; }
        public IReadOnlyDictionary<string, object?> Properties { get; init; }

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

    public Node(string type, IReadOnlyDictionary<string, object?> properties)
        {
            Id = ++_idCouter;
            Type = type;
            Properties = properties;
        }
        public T? GetProperty<T>(string name)
        {
            
            if (Properties.TryGetValue(name, out var property))
            {
                if(property is JsonElement element)
                {
                    var parsed = JsonSerializer.Deserialize<T>(element, options);
                    return parsed;
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

