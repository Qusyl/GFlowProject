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
    public class DeleteSqlQueryHandler : SqlQueryHandlerBase, ISqlQueryHandler<DeleteDefinition>
    {
        public async Task<QueryResult> HandleAsync(DeleteDefinition def, IDbConnection connection, ISqlDialect dialect)
        {
            var table = dialect.QuoteIdentifier(def.TableName);

            var (whereSql, parameters) = BuildWhere(def.Where, dialect);
           
            var normalizedParameters = Normalize(parameters);
            if (string.IsNullOrEmpty(whereSql))
            {
                throw new InvalidOperationException("DELETE without WHERE not supported!");
            }
            var sql = $"DELETE  FROM {table} {whereSql}";

            var affected = await connection.ExecuteAsync(sql, normalizedParameters);

            return new QueryResult(affected);
        }
        private Dictionary<string, object?> Normalize(Dictionary<string, object?> values)
        {
            Dictionary<string, object?> normValues = new();

            foreach (KeyValuePair<string, object?> kvp in values)
            {
                if (kvp.Value is JsonElement element)
                {
                    switch (element.ValueKind)
                    {
                        case JsonValueKind.String: normValues.Add(kvp.Key, element.GetString()); break;
                        case JsonValueKind.Number: normValues.Add(kvp.Key, element.GetDecimal()); break;
                        case JsonValueKind.True: normValues.Add(kvp.Key, true); break;
                        case JsonValueKind.False: normValues.Add(kvp.Key, false); break;
                        case JsonValueKind.Null: normValues.Add(kvp.Key, null); break;
                        default: throw new NotSupportedException($"Not supported type {element.ValueKind}");
                    }
                }
                else
                {
                    normValues.Add(kvp.Key, kvp.Value);
                }
            }
            return normValues;
        } 
    
    }
}