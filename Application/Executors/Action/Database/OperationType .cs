using System.Text.Json.Serialization;

namespace Application.Executors.Action.Database
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationType
    {
        Raw,

        Insert,
        Select,
        Update,

        Delete
    }
}