using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Executors.Action.Http
{
    public sealed record FileAttachment(
        string? FilePath,

        byte[] FileBytes,

        string? FileName,

        string? ParameterName
    );
}