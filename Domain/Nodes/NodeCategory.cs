using System.Text.Json.Serialization;

namespace Domain.Nodes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NodeCategory
    {
        Trigger,
        Logic,
        Action,
        Workflow
    }
}