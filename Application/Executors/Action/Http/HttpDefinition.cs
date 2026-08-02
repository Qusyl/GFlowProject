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
            return (T)argument;
        }
        throw new NullReferenceException($"Not found argument {name}");
    }
};