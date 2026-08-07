using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Application.Executors.Action.Database.Definitions;
using Application.Executors.Action.Database.Dialects;
using System.Text.Json;

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
            var values = Normalize(def.Values);
            var affected = await connection.ExecuteAsync(sql, values);
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