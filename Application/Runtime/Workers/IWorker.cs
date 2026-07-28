
using Domain.Nodes;
namespace Application.Runtime.Workers;

public interface IWorker
{
    Task<NodeResult> ProcessAsync(CancellationToken cts = default);
    
}
