using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Visitor;
using Application.Executors.Action.Database.Visitor.Conditions;

namespace Application.Executors.Action.Database;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(ComparisonCondition), "comparison")]
[JsonDerivedType(typeof(GroupCondition), "group")]
[JsonDerivedType(typeof(NotCondition), "not")]
public abstract class QueryCondition
{
    public abstract T Accept<T>(IQueryConditionVisitor<T> visitor);
}
