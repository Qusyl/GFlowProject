using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database
{
    public abstract record QueryDefinition
    {
        public abstract OperationType Operation { get; }

        public string TableName { get; }
    }
}