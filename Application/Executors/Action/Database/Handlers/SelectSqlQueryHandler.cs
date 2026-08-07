using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;
using Dapper;

namespace Application.Executors.Action.Database.Handlers
{
    public class SelectSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<SelectDefinition>
    {
       

        public async Task<QueryResult> HandleAsync(SelectDefinition def, IDbConnection connection, ISqlDialect dialect)
        {
            var table = dialect.QuoteIdentifier(def.TableName);

            var columns = def.Columns.Any() ? string.Join(", ", def.Columns.Select(dialect.QuoteIdentifier)) : "*";

            var (wheresql, parameters) = BuildWhere(def.Where, dialect);

            var sql = $"SELECT {columns} FROM {table} {wheresql}";

            var rows = await connection.QueryAsync(sql, parameters);

            return new QueryResult(rows);
        }
    }
}