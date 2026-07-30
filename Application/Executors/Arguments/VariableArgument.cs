using System.Text.Json.Serialization;

namespace Application.Executors.Logic.Logics;

public sealed record VariableArgument(string VariableName) : LogicArgument;
