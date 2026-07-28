using System.Text.Json.Serialization;

namespace Domain.Ports
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PortsDirection
    {
        Input,
        Output
    }
}