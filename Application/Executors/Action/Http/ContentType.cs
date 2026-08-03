using System.Text.Json.Serialization;

namespace Application.Executors.Action.Http
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentType
    {
        Json,
        Xml,

        FormUrlEncoded,
        MultipartForm,

        Binary
    }
}