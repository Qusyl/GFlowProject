using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;
using Dapper;

namespace Application.Executors.Action.Database.Handlers
{
    public class UpdateSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<UpdateDefinition>
    {


        public async Task<QueryResult> HandleAsync(UpdateDefinition def, IDbConnection connection, ISqlDialect dialect)
        {
            var table = dialect.QuoteIdentifier(def.TableName);

            var setParamNames = new Dictionary<string, object?>();

            var setCloses = def.Values.Select(kvp =>
            {

                var paramName = $"V_{kvp.Key}";
                setParamNames[paramName] = NormalizeObject(kvp.Value);
                return $"{dialect.QuoteIdentifier(kvp.Key)} = {dialect.ParameterPrefix}{paramName}";
            });
            var (whereSql, parameters) = BuildWhere(def.Where, dialect);

            foreach (var kvp in parameters)
            {
                setParamNames[kvp.Key] = NormalizeObject(kvp.Value);
            }

            var sql = $"UPDATE {table} SET {string.Join(", ", setCloses)} {whereSql}";

            var affected = await connection.ExecuteAsync(sql, setParamNames);

            return new QueryResult(affected);
        }

        public object? NormalizeObject(object? value)
        {
            if (value is not JsonElement element)
            {
                return value;
            }

            return element.ValueKind switch
            {
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.GetDecimal(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                _=> throw new NotSupportedException($"Not supported JsonValueKind {element.ValueKind}")
            };
        }
    }
}