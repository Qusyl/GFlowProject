using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services.Schemas.Providers
{
    public class SqlProvider : INodeSchemaProvider
    {
        public string NodeType => "sql";

        public NodeTypeSchema GetSchema()
        {
            
        }
    }
}