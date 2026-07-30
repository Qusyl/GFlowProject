using System.Collections.Concurrent;
using Application.Executors.Configurations;

namespace Application.Runtime.Workers;

public sealed class WorkerPool
{
    private readonly ConcurrentBag<IWorker> _pool;

    private readonly ExecutorRegistry _registry;

    private readonly SemaphoreSlim _semaphore; 

    private readonly int _maxBufferSize;

    private int _busyWorkers = 0;

    private int _createdWorkers = 0;

    public int BusyWorkers => _busyWorkers;
    private WorkerPool(int capacity, int maxConcurrentWorkers)
    {
        _pool = new ConcurrentBag<IWorker>();

        _maxBufferSize = capacity;

        _semaphore = new SemaphoreSlim(maxConcurrentWorkers);

        _registry = new();
    }

    public static WorkerPool Create(int poolMaxSize, int concurrentWorkersCount = 2) => new(poolMaxSize, concurrentWorkersCount);

    public async Task<IWorker> AcquireAsync(CancellationToken cts)
    {
        await _semaphore.WaitAsync(cts);

        if (_pool.TryTake(out var worker))
        {
            Interlocked.Increment(ref _busyWorkers);
            return worker;
        }
        IWorker? createdWorker = null;
        if (_createdWorkers < _maxBufferSize)
        {
            createdWorker = new SessionWorker(_registry);

            Interlocked.Increment(ref _createdWorkers);
            Interlocked.Increment(ref _busyWorkers);
        }
        if (createdWorker is null)
        {
            throw new NullReferenceException("WorkerPoolException: Semaphor can't occured null reference worker");
        }
        return createdWorker;
    }
    
    public void ReturnWorker(IWorker worker)
    {
        _pool.Add(worker);

        Interlocked.Decrement(ref _busyWorkers);

        _semaphore.Release();
    }
}