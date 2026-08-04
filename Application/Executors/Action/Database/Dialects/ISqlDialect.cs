using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database.Dialects;

    public interface ISqlDialect
    {
        string QuoteIdentifier(string name);
        string ParameterPrefix { get; }

        string LikeOperator(bool caseSensetive);

        string FormatBoolean(bool value);
    }
