using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Dialects;
using Application.Executors.Action.Database.Visitor;

namespace Application.Executors.Action.Database.Handlers
{
    public abstract class SqlQueryHandlerBase
    {
        protected (string Sql, Dictionary<string, object?> Parameters) BuildWhere(QueryCondition? condition, ISqlDialect dialect)
        {
            var parameters = new Dictionary<string, object?>();

            if (condition is null)
            {
                return ("", parameters);
            }
            var visitor = new SqlConditionVisitor(dialect, parameters);

            var close = condition.Accept(visitor);

            return ($"WHERE {close}", parameters);
        }
    }   
}