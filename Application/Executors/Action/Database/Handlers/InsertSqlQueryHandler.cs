using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;

namespace Application.Executors.Action.Database.Handlers
{
    public class InsertSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<InsertDefinition>
    {
       

        public async Task<QueryResult> HandleAsync(InsertDefinition def, IDbConnection connection, ISqlDialect dialect)
        {

            var table = dialect.QuoteIdentifier(def.TableName);
            var columns = string.Join(", ", def.Values.Keys.Select(dialect.QuoteIdentifier));
            var parameters = string.Join(", ", def.Values.Keys.Select(k => $"{dialect.ParameterPrefix}{k}"));
            var sql = $"INSERT INTO {table} ({columns}) VALUES ({parameters})";
            var affected = await connection.ExecuteAsync(sql);
            return new QueryResult(affected);
        }
    }
}