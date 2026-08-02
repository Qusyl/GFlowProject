namespace Application.Executors.Configurations;

public interface IExecutorResolver
{
    INodeExecutors? Resolve(string typeName);
}