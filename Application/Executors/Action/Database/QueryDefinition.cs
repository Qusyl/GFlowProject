using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Definitions;

namespace Application.Executors.Action.Database
{

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "operation")]
    [JsonDerivedType(typeof(SelectDefinition), "Select")]
    [JsonDerivedType(typeof(InsertDefinition), "Insert")]
    [JsonDerivedType(typeof(UpdateDefinition), "Update")]
    [JsonDerivedType(typeof(DeleteDefinition), "Delete")]
    [JsonDerivedType(typeof(RawDefinition), "Raw")]
    public abstract record QueryDefinition
    {
        public abstract OperationType Operation { get; }

        public string TableName { get; }
    }
}