using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Visitor.Conditions;

namespace Application.Executors.Action.Database.Visitor
{
    public interface IQueryConditionVisitor<T>
    {
        T VisitCompare(ComparisonCondition condition);

        T VisitGroup(GroupCondition condition);

        T VisitNot(NotCondition condition);
    }
}