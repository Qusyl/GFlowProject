using Domain.Ports;
namespace Application;

public sealed class NodeResult
{
    public IReadOnlyCollection<PortsDescriptor>? Ports { get; init; }

    public Exception? Error { get; init; }

    public bool IsSuccess { get; init; }

    private NodeResult(IReadOnlyCollection<PortsDescriptor>? result, Exception? error, bool isSuccess)
    {
        Ports = result;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static NodeResult Success(IReadOnlyCollection<PortsDescriptor>? ports) => new(ports, default, true);

    public static NodeResult Failure(Exception? error) => new(default, error, false);
}
