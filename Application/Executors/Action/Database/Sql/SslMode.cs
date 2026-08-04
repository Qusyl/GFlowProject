using System.Text.Json.Serialization;

namespace Application.Executors.Action.Database.Sql
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SslMode
    {
        Disabled
    }
}