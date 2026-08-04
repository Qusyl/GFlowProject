using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Definitions
{
    public sealed record SelectDefinition : QueryDefinition
    {
        public override OperationType Operation => OperationType.Select;

        public List<string> Columns { get; set; } = new();

        public QueryCondition Where { get; set; }
    }
}