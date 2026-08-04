using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Dialects
{
    public class PsqlDialect : ISqlDialect
    {
        public string ParameterPrefix => "@";

        public string FormatBoolean(bool value)
        {
            return value ? "TRUE" : "FALSE";
        }

        public string LikeOperator(bool caseSensetive)
        {
            return caseSensetive ? "LIKE" : "ILIKE";
        }

        public string QuoteIdentifier(string name)
        {
             return $"\"{name}\""; 
        }
    }
}