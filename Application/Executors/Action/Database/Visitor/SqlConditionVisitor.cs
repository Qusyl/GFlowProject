using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Dialects;
using Application.Executors.Action.Database.Visitor.Conditions;
using Application.Executors.Action.Database.Visitor.Operators;

namespace Application.Executors.Action.Database.Visitor
{
    public class SqlConditionVisitor : IQueryConditionVisitor<string>
    {
        private readonly ISqlDialect _sqlDialect;
        private readonly Dictionary<string, object?> _parameters;

        private int _parameterCounter = 0;

        public SqlConditionVisitor(ISqlDialect sqlDialect, Dictionary<string, object?> parameters)
        {
            _sqlDialect = sqlDialect;
            _parameters = parameters;
        } 
        public string VisitCompare(ComparisonCondition condition)
        {
            var paramName = $"p{_parameterCounter++}";
            _parameters[paramName] = condition.Value;
            var field = _sqlDialect.QuoteIdentifier(condition.Field);
            var op = condition.Operator switch
            {
                ComparisonOperator.Equal => "=",
                ComparisonOperator.GreaterThan => ">",
                ComparisonOperator.LessThan => "<",
                ComparisonOperator.GreaterThanOrEqual => ">=",
                ComparisonOperator.LessThanOrEqual => "<=",
                ComparisonOperator.Like => _sqlDialect.LikeOperator(caseSensetive: false),
                ComparisonOperator.In => "IN",
                ComparisonOperator.IsNull => "IS NULL",
                ComparisonOperator.NotEqual => "<>",
                _ => throw new NotSupportedException("Not supported operation ")
            };

            return $"{field} {op} {_sqlDialect.ParameterPrefix}{paramName}";
        }

        public string VisitGroup(GroupCondition condition)
        {
            var op = condition.Operator == LogicalOperator.And ? "AND" : "OR";

            var parts = condition.Conditions.Select(c => c.Accept(this));

            return $"({string.Join($" {op} ", parts)})";
        }

        public string VisitNot(NotCondition condition)
        {
            return $"NOT ({condition.Inner.Accept(this)})";
        }
    }
}