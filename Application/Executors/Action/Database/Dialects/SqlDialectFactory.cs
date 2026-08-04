using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Dialects
{
    public class SqlDialectFactory : ISqlDialectFactory
    {
        public ISqlDialect GetDialect(DatabaseType dbtype) => dbtype switch
        {
            DatabaseType.Postgresql => new PsqlDialect(),
            DatabaseType.Mysql => new MySqlDialect(),
            _ => throw new NotSupportedException($"Not supported database {nameof(dbtype)}")
        };
    }
}