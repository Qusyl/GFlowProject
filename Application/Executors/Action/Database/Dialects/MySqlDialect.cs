using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Dialects
{
    public class MySqlDialect : ISqlDialect
    {
        public string ParameterPrefix => "@";

        public string FormatBoolean(bool value)
        {
            return value ? "1" : "0";
        }

        public string LikeOperator(bool caseSensetive)
        {
            return "LIKE";
        }

        public string QuoteIdentifier(string name)
        {
            return $"`{name}`";
        }
    }
}