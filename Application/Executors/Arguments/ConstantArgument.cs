using System.Text.Json.Serialization;

namespace Application.Executors.Logic.Logics;

public sealed record ConstantArgument(object Value, ArgumentType Type) : LogicArgument;