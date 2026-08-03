using System.Text.Json;
using System.Text.Json.Serialization;
using Application.JsonOptions;

namespace Application.Executors.Action.Http;

public sealed record HttpDefinition(

    HttpMethodType MethodType,
    ContentType ContentType,
    AuthDefinition Auth,

    Dictionary<string, object> Arguments,

    List<FileAttachment>? Files,

    Dictionary<string, string>? FormData,

    int RetriesCount,

    TimeSpan RetryDelay
)
{
    public T GetArgument<T>(string name)
    {
        if (Arguments.TryGetValue(name, out var argument))
        {
            if (argument is JsonElement jsonElement)
            {
                var parsed = jsonElement.Deserialize<T>();
                if (parsed is null)
                {
                    throw new InvalidCastException($"Can't convert argument {name} to type {typeof(T)}");
                }
                return parsed;
            }
            if (argument is T validForReturn)
            {
                return validForReturn;
            }
            try
            {
                return (T)Convert.ChangeType(argument, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot convert argument '{name}' to {typeof(T)}", ex);
            }
        }
        throw new NullReferenceException($"Not found argument type for {name}");
    }
    
};