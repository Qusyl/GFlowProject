using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Visitor.Conditions
{
    public class NotCondition : QueryCondition
    {
        public QueryCondition Inner { get; set; } = default!;
        public override T Accept<T>(IQueryConditionVisitor<T> visitor)
        {
            return visitor.VisitNot(this);
        }
    }
}