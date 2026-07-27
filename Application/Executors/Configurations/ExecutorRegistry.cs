using System.Reflection;


namespace Application.Executors.Configurations
{

    public sealed class ExecutorRegistry
    {
        private readonly Dictionary<string, Type> _cached;
        public ExecutorRegistry()
        {
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
