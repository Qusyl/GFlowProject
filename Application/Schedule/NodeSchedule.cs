using Application.ReadyQueue;
using Microsoft.Extensions.Hosting;

namespace Application.Scheldure;

public class NodeSchedule : BackgroundService
{
    private readonly IReadyQueue _readyQueue;

    public NodeSchedule(IReadyQueue _queue)
    {
        _readyQueue = _queue;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}
