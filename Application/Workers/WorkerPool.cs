using System.Collections.Concurrent;
using Application.Runtime.Workers;

namespace Application.Workers;

public class WorkerPool
{
    private readonly ConcurrentBag<IWorker> _pool;
    private readonly int _maxBufferSize;

    private WorkerPool(int maxBufferSize)
    {
        _pool = new ConcurrentBag<IWorker>();
        _maxBufferSize = maxBufferSize;
    }
    public WorkerPool() { }

    public static WorkerPool Create(int maxBuffer) => new(maxBuffer);
    public IWorker? Get()
    {
        if (_pool.TryTake(out IWorker? worker))
        {
            return worker;
        }
        return default;
    }

    public void Return(IWorker woker)
    {
        if (woker == null)
        {
            return;
        }
        if(_pool.Count < _maxBufferSize)
        {
            _pool.Add(woker);
        }
    }
}
