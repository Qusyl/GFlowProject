using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Definitions
{
    public sealed record RawDefinition : QueryDefinition
    {
        public override OperationType Operation => OperationType.Raw;

        public Dictionary<string, object?> Parameters { get; set; } = new();

        public string Query { get; set; } = default!;
    }
}