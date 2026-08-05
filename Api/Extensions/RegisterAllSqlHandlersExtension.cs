using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database.Handlers;

namespace Api.Extensions
{
    public static class RegisterAllSqlHandlersExtension
    {
        public static IServiceCollection RegisterAllSqlHandlers(this IServiceCollection services)
        {
            var handlerInterfType = typeof(ISqlQueryHandler<>);

            var handlerRegistrs = typeof(ISqlQueryHandler<>).Assembly
            .GetTypes()
            .Where(t => !t.IsAbstract && t.IsClass)
            .SelectMany(impl => impl
                .GetInterfaces()
                .Where(interf => interf.IsGenericType && interf.GetGenericTypeDefinition() == handlerInterfType)
                .Select(assmbledInterface => new { assmbledInterface, impl}));
            
            foreach(var registration in handlerRegistrs)
            {
                services.AddTransient(registration.assmbledInterface, registration.impl);
            }

            return services;
        }
    }
}