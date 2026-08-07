using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Definitions
{
    public sealed record InsertDefinition : QueryDefinition
    {
        public override OperationType Operation => OperationType.Insert;

        public Dictionary<string, object?> Values { get; set; } = new();
    }
}