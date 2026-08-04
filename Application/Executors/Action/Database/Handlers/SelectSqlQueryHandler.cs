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
        public SelectSqlQueryHandler(ISqlDialect dialect) : base(dialect)
        {
        }

        public async Task<QueryResult> HandleAsync(SelectDefinition def, IDbConnection connection)
        {
            var table = Dialect.QuoteIdentifier(def.TableName);

            var columns = def.Columns.Any() ? string.Join(", ", def.Columns.Select(Dialect.QuoteIdentifier)) : "*";

            var (wheresql, parameters) = BuildWhere(def.Where);

            var sql = $"SELECT {columns} FROM {table} {wheresql}";

            var rows = await connection.ExecuteAsync(sql);

            return new QueryResult(rows);
        }
    }
}