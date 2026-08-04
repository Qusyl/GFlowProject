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
        protected readonly ISqlDialect Dialect;

        protected SqlQueryHandlerBase(ISqlDialect dialect)
        {
            Dialect = dialect;
        }
        protected (string Sql, Dictionary<string, object?> Parameters) BuildWhere(QueryCondition? condition)
        {
            var parameters = new Dictionary<string, object?>();

            if (condition is null)
            {
                return ("", parameters);
            }
            var visitor = new SqlConditionVisitor(Dialect, parameters);

            var close = condition.Accept(visitor);

            return ($"WHERE {close}", parameters);
        }
    }   
}