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
    public class UpdateSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<UpdateDefinition>
    {
        public UpdateSqlQueryHandler(ISqlDialect dialect) : base(dialect)
        {
        }

        public async Task<QueryResult> HandleAsync(UpdateDefinition def, IDbConnection connection)
        {
            var table = Dialect.QuoteIdentifier(def.TableName);

            var setParamNames = new Dictionary<string, object?>();

            var setCloses = def.Values.Select(kvp =>
            {

                var paramName = $"V_{kvp.Key}";
                setParamNames[paramName] = kvp.Value;
                return $"{Dialect.QuoteIdentifier(kvp.Key)} = {Dialect.ParameterPrefix}{paramName}";
            });
            var (whereSql, parameters) = BuildWhere(def.Where);

            foreach (var kvp in parameters)
            {
                setParamNames[kvp.Key] = kvp.Value;
            }

            var sql = $"UPDATE {table} SET {string.Join(", ", setCloses)} {whereSql}";

            var affected = await connection.ExecuteAsync(sql);

            return new QueryResult(affected);
        }
    }
}