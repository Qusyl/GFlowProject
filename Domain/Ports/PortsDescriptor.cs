namespace Domain.Ports
{
    public sealed record PortsDescriptor(
        int Id,
        string PortName,
        PortsDirection PortDirection);
    
}