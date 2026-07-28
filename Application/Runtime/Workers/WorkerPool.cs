using System.Collections.Concurrent;
using Application.Executors.Configurations;

namespace Application.Runtime.Workers;

public sealed class WorkerPool
{
    private readonly ConcurrentBag<IWorker> _pool;

    private readonly ExecutorRegistry _registry; 

    private int _maxBufferSize;

    private int _busyWorkers = 0;

    private int _createdWorkers = 0;

    public int BusyWorkers => _busyWorkers;
    private WorkerPool(int capacity)
    {
        _pool = new ConcurrentBag<IWorker>();

        _maxBufferSize = capacity;

        _registry = new();
    }

    public static WorkerPool Create(int poolMaxSize) => new(poolMaxSize);

    public IWorker? RentWorker()
    {
        if (_pool.TryTake(out IWorker? worker))
        {
            _busyWorkers++;
            return worker;
        }

        if (_pool.Count < _maxBufferSize)
        {
            var newWorker = new SessionWorker(_registry);

            _busyWorkers++;

            return newWorker;
        }
        
        return default;
    }

    public void ReturnWorker(IWorker? worker)
    {
        if (worker is null)
        {
            return;
        }
        if(_pool.Count < _maxBufferSize)
        {
            _pool.Add(worker);
            _busyWorkers--;
        }
    }
}