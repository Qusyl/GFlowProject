using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Definitions
{
    public sealed record DeleteDefinition : QueryDefinition
    {
        public override OperationType Operation => OperationType.Delete;

        public QueryCondition Where { get; set; }

    }
}