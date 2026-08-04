using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Definitions
{
    public sealed record UpdateDefinition : QueryDefinition
    {
        public override OperationType Operation => OperationType.Update;

        public Dictionary<string, object?> Values { get; set; } = new();

        public QueryCondition Where { get; set; }

    }
}