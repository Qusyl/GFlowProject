using Domain.Ports;
namespace Domain.Nodes
{
    public sealed record NodeDescriptor(
        string DisplayName,
         NodeCategory NodeCategory,
          IReadOnlyCollection<PortsDescriptor> Ports);

    
}