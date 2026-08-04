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
    public class DeleteSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<DeleteDefinition>
    {
        public DeleteSqlQueryHandler(ISqlDialect dialect) : base(dialect)
        {
        }

        public async Task<QueryResult> HandleAsync(DeleteDefinition def, IDbConnection connection)
        {
            var table = Dialect.QuoteIdentifier(def.TableName);

            var (whereSql, parameters) = BuildWhere(def.Where);

            if (string.IsNullOrEmpty(whereSql))
            {
                throw new InvalidOperationException("DELETE without WHERE not supported!");
            }

            var sql = $"DELETE {parameters} FROM {table} {whereSql}";

            var affected = await connection.ExecuteAsync(sql);

            return new QueryResult(affected);
        }
    }
}