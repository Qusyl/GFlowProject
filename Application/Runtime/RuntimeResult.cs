namespace Application.Runtime;

public sealed class RuntimeResult
{
    public bool IsSuccess { get; }
    public Exception? Exception { get; }

    private RuntimeResult(bool isSuccess, Exception? exception)
    {
        IsSuccess = isSuccess;
        Exception = exception;
    }

    public static RuntimeResult Success => new(true, default);

    public static RuntimeResult Failure(Exception ex) => new(false, ex);
}

