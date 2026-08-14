using System.Text.Json.Serialization;
using Domain.Ports;
namespace Domain.Nodes
{
    public sealed record NodeDescriptor(
          string DisplayName,
        
          NodeCategory NodeCategory,
          IList<PortsDescriptor> Ports)
    {
        public bool HasOutput(string portName) => Ports.Any(p => (p.PortName== portName) && (p.PortDirection is PortsDirection.Output));
        public bool HasInput(string portName) => Ports.Any(p => (p.PortName == portName) && (p.PortDirection is PortsDirection.Input));

        public IReadOnlyCollection<PortsDescriptor>? GetIncomingPorts() => Ports.Where(p =>  p.PortDirection is PortsDirection.Input)?.ToList();
        public IReadOnlyCollection<PortsDescriptor>? GetOutcomingPorts() => Ports.Where(p =>  p.PortDirection is PortsDirection.Output)?.ToList();
    };
}