using System.Reflection;
using Application.Executors.Configurations;

namespace MainApi.Extensions;

public static class RegisterAllExecutorsExtension
{
    public static IServiceCollection RegisterAllExecutors(this IServiceCollection services)
    {

        foreach (var type in typeof(INodeExecutors).Assembly.GetTypes()
        .Where(t => typeof(INodeExecutors).IsAssignableFrom(t) && !t.IsAbstract))
        {
            services.AddTransient(type);
        }

        return services;
    }
}