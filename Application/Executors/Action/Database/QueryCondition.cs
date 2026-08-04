using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Visitor;

namespace Application.Executors.Action.Database;

    public abstract class QueryCondition
    {
        public abstract T Accept<T>(IQueryConditionVisitor<T> visitor);
    }
