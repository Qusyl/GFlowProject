using Domain.Nodes;
using Domain.Ports;

namespace Domain.Graph
{
    public sealed record Edge(int NodeFromId, int NodeToId, string PortFrom, string PortTo );

}