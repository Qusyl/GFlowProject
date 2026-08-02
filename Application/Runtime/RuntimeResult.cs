namespace Application.Runtime;

public sealed class RuntimeResult
{

    public IReadOnlyCollection<Exception>? Exceptions { get; init; }
    public RuntimeResult(IReadOnlyCollection<Exception>? exceptions )
    {
        Exceptions = exceptions;
    }

}

