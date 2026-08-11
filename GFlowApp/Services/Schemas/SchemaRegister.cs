using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFlowApp.Services.Schemas
{
    public sealed class SchemaRegister
    {
        private readonly Dictionary<string, INodeSchemaProvider> _providers;

        public SchemaRegister(IEnumerable<INodeSchemaProvider> providers)
        {
            _providers = providers.ToDictionary(kvp => kvp.NodeType);
        }
        
        public bool TryGet(string nodeType, out NodeTypeSchema? schema)
        {
            if (_providers.TryGetValue(nodeType, out var service))
            {
                schema = service.GetSchema();
                return true;
            }
            schema = null;
            return false;
        }
    }
}