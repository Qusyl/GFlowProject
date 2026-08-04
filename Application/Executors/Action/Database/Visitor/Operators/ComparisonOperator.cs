using System.Text.Json.Serialization;

namespace Application.Executors.Action.Database.Visitor.Operators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ComparisonOperator
    {
        GreaterThan,
        LessThan,

        Equal,

        NotEqual,

        GreaterThanOrEqual,

        LessThanOrEqual,

        Like,

        In,

        IsNull
    }
}