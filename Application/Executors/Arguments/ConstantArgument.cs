using System.Text.Json.Serialization;
using Application.JsonOptions;

namespace Application.Executors.Logic.Logics;

public sealed record ConstantArgument([property: JsonConverter(typeof(ValueJsonConverter))]object Value, ArgumentType Type) : LogicArgument;