using Domain.Ports;
namespace Domain.Nodes
{
    public sealed record NodeDescriptor(
          int Id,
          string DisplayName,
          NodeCategory NodeCategory,
          IReadOnlyCollection<PortsDescriptor> Ports)
    {
        public bool HasOutput(int portId) => Ports.Any(p => (p.Id == portId) && (p.PortDirection is PortsDirection.Output));
        public bool HasInput(int portId) => Ports.Any(p => (p.Id == portId) && (p.PortDirection is PortsDirection.Input));

        public IReadOnlyCollection<PortsDescriptor>? GetIncomingPorts() => Ports.Where(p =>  p.PortDirection is PortsDirection.Input)?.ToList();
        public IReadOnlyCollection<PortsDescriptor>? GetOutcomingPorts() => Ports.Where(p =>  p.PortDirection is PortsDirection.Output)?.ToList();
    };
}