namespace Application.Queues.Exception;


public interface IExceptionQueue
{
    bool Empty { get;}
        ValueTask<System.Exception> ReadAsync(CancellationToken cts = default);

    ValueTask WriteAsync(System.Exception value, CancellationToken cts = default);

    Task<IReadOnlyCollection<System.Exception>> ReadAllAsync(CancellationToken cts = default);
}