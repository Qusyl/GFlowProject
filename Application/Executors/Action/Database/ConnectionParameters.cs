using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database;

namespace Application.Executors.Action.Database
{
    public sealed record ConnectionParameters
    (
        string Host,
        int? Port,
        string Username,

        string Password,

        string Database,

        int? TimeOut,

        bool? Pooling,

        SslMode SslMode,

        int? MinPoolSize,

        int? MaxPoolSize,

        string? SearchPath,

        bool? AllowUserVariables,

        string CharacterSet
    );
}