using System.Text.Json.Serialization;

namespace GFlowApp.Services
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BlockTypes
    {
        SqlAction,
        HttpAction,

        MessageAction,

        EmailAction,

        CompareLogic,

        ManualTrigger
    }
}