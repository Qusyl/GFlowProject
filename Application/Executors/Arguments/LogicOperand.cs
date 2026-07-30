using System.Text.Json.Serialization;

namespace Application.Executors.Logic.Logics;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum LogicOperand
{
    GreaterThan,
    LessThan,
    EqualTo,

    GraterThanOrEqual,

    LessThanOrEqual
}