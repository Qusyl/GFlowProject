using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GFlowApp.Services.Schemas
{
    public sealed class SchemaRegister : ISchemaRegister
    {
        private readonly Dictionary<string, INodeSchemaProvider> _providers = new Dictionary<string, INodeSchemaProvider>();

        private readonly IServiceProvider _services;

        public SchemaRegister(IServiceProvider services)
        {
            _services = services;
            var providerTypes = typeof(INodeSchemaProvider)
            .Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsClass && typeof(INodeSchemaProvider)
            .IsAssignableFrom(t));

            foreach(var type in providerTypes)
            {
                var provider = (INodeSchemaProvider)ActivatorUtilities.CreateInstance(_services, type);
                if(provider is not null)
                {
                    _providers.Add(provider.NodeType, provider);
                }
            }
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