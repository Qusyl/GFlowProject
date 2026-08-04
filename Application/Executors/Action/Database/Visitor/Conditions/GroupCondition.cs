using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Visitor.Operators;

namespace Application.Executors.Action.Database.Visitor.Conditions
{
    public class GroupCondition : QueryCondition
    {
        public LogicalOperator Operator { get; set; }

        public List<QueryCondition> Conditions { get; set; } = new();

        public override T Accept<T>(IQueryConditionVisitor<T> visitor)
        {
            return visitor.VisitGroup(this);
        }
    }
}