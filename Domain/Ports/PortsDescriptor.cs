namespace Domain.Ports
{
    public sealed record PortsDescriptor(
        string PortName,
        PortsDirection PortDirection);
}