using System.Reflection;
using Application.Executors.Configurations;


namespace Application.Executors.Configurations
{

    public sealed class ExecutorRegistry : IExecutorResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _cached;
        public ExecutorRegistry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _cached = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass
            && !t.IsAbstract
            && t.IsDefined(typeof(NodeExecutorAttribute)))
            .ToDictionary(
                t => t.GetCustomAttribute<NodeExecutorAttribute>()!.Type,
                t => t
            );
        }
        public INodeExecutors? Resolve(string typeName)
        {
            if (_cached.TryGetValue(typeName, out var type))
            {
                return Activator.CreateInstance(type) as INodeExecutors;
            }

            return null;
        }
    }
}
