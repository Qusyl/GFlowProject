using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Database
{
    public record DatabaseConfiguration
    (
        string DatabaseType,
        ConnectionParameters ConnectionParameters
    );
}