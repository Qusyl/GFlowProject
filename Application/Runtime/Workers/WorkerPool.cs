using System.Collections.Concurrent;
using Application.Executors.Configurations;

namespace Application.Runtime.Workers;

public sealed class WorkerPool
{
    private readonly ConcurrentBag<IWorker> _pool;

    private readonly IExecutorResolver _registry;

    private readonly SemaphoreSlim _semaphore; 

    private readonly int _maxBufferSize;

    private int _busyWorkers = 0;

    private int _createdWorkers = 0;

    private int _totalTasksProccessing = 0;

    public int BusyWorkers => Interlocked.CompareExchange(ref _busyWorkers, 0, 0);

    public int TotalTaskProccessing => Interlocked.CompareExchange(ref _totalTasksProccessing, 0, 0);
    private WorkerPool(IExecutorResolver registry, int capacity, int maxConcurrentWorkers)
    {
        _pool = new ConcurrentBag<IWorker>();

        _maxBufferSize = capacity;

        _semaphore = new SemaphoreSlim(maxConcurrentWorkers);

        _registry = registry;

    }

    public static WorkerPool Create(int poolMaxSize, IExecutorResolver registry,int concurrentWorkersCount = 2) => new(registry,poolMaxSize, concurrentWorkersCount);

    public async Task<IWorker> AcquireAsync(CancellationToken cts)
    {
        await _semaphore.WaitAsync(cts);

        Interlocked.Increment(ref _totalTasksProccessing);

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
            _semaphore.Release();
            Interlocked.Decrement(ref _totalTasksProccessing);
            throw new NullReferenceException("WorkerPoolException: Semaphor can't occured null reference worker");
        }
        return createdWorker;
    }

    public void ReturnWorker(IWorker worker)
    {
        _pool.Add(worker);

        Interlocked.Decrement(ref _busyWorkers);
        Interlocked.Decrement(ref _totalTasksProccessing);

        _semaphore.Release();
    }

    public bool IsIdle() => _totalTasksProccessing == 0;
}