using System.Reflection;
using Application.Executor;
using Application.Executor.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Executors.Configuration;

public  class ExecutorRegistry
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
   public INodeExecutor? Resolve(string typeName)
    {
        if (_cached.TryGetValue(typeName, out var type))
        {
            return Activator.CreateInstance(type) as INodeExecutor;
        }

        return null;
    }
}
