using System.Text.Json.Serialization;

namespace Application.Executors.Action.Database
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SslMode
    {
        None,
        Required,

        VerifyCA,
        VerifyFull

    }
}