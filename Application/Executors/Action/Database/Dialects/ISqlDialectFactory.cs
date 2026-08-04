using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Dialects
{
    public interface ISqlDialectFactory
    {
        ISqlDialect GetDialect(DatabaseType dbtype);
    }
}