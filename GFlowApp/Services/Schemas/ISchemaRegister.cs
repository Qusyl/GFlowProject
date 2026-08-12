using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services.Schemas
{
    public interface ISchemaRegister
    {
        bool TryGet(string nodeType, out NodeTypeSchema? schema);
    }
}