using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;

namespace Application.Executors.Action.Database.Handlers
{
    public interface ISqlQueryHandler
    {
        Task<QueryResult> HandleAsync(RawDefinition def, IDbConnection connection, ISqlDialect dialect);
    }
}