using System.Text.Json.Serialization;

namespace Application.Executors.Logic.Logics;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ArgumentType
{
    Integer,
    Double,

    Decimal,

    String,

    Enum,

    Boolean,

    DateTime,

    HttpResponseMessage

}