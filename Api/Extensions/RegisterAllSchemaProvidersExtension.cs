using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GFlowApp.Services;

namespace Api.Extensions
{
    public static  class RegisterAllSchemaProvidersExtension
    {
        public static IServiceCollection RegisterAllSchemaProviders(this IServiceCollection services)
        {
            var types = typeof(INodeSchemaProvider)
            .Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsClass && typeof(INodeSchemaProvider)
            .IsAssignableFrom(t))
            .ToList();
            foreach (var type in types)
            {
                if (type is not null)
                {
                    services.AddTransient(type);
                }
            }

            return services;
        }
    }
}