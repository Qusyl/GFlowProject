using System.Text.Json.Serialization;

namespace Application.Executors.Logic.Logics;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(VariableArgument), nameof(VariableArgument))]
[JsonDerivedType(typeof(ConstantArgument), nameof(ConstantArgument))]
public abstract record LogicArgument;