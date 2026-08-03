using System.Text.Json.Serialization;

namespace Application.Executors.Action.Http;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HttpMethodType
{
    GET,
    POST,

    UPDATE,

    DELETE
}