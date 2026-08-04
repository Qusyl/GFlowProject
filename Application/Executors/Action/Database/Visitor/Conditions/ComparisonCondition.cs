using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Visitor.Operators;

namespace Application.Executors.Action.Database.Visitor.Conditions
{
    public class ComparisonCondition : QueryCondition
    {
        public string Field { get; set; }

        public object? Value { get; set; }

        public ComparisonOperator Operator { get; set; }
        public override T Accept<T>(IQueryConditionVisitor<T> visitor)
        {
            return visitor.VisitCompare(this);
        }
    }
}