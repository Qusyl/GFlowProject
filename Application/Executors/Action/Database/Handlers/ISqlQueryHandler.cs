using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Handlers
{
    public interface ISqlQueryHandler<TDefinition> where TDefinition : QueryDefinition
    {
        Task<QueryResult> HandleAsync(TDefinition def, IDbConnection connection);
    }
}