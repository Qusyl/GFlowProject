using Domain.Nodes;

namespace Domain.Graph;

public class ExecutionGraph
{
    private readonly Dictionary<int, Node> _nodes;

    private readonly Dictionary<int, List<Edge>> _incomingEdges;

    private readonly Dictionary<int, List<Edge>> _outComingEdges;

    public IReadOnlyDictionary<int, Node> Nodes => _nodes.AsReadOnly();

    public IReadOnlyDictionary<int, List<Edge>> Incoming => _incomingEdges.AsReadOnly();

    public IReadOnlyDictionary<int, List<Edge>> Outcoming => _outComingEdges.AsReadOnly();
   
    public ExecutionGraph(Dictionary<int, Node> nodes,Dictionary<int, List<Edge>> incomingEdges, Dictionary<int, List<Edge>> outComingEdges)
    {
        _nodes = nodes;
        _incomingEdges = incomingEdges;
        _outComingEdges = outComingEdges;
    }
    public Node? GetNode(int id)
    {
        if (_nodes.TryGetValue(id, out var node))
        {
            return node;
        }
        return default;
    }
    public IReadOnlyCollection<Edge>? GetOutcoming(int nodeId)
    {
        if (_outComingEdges.TryGetValue(nodeId, out List<Edge>? edges))
        {
            return edges;
        }
        return default;
    }
    public IReadOnlyCollection<Edge>? GetIncoming(int nodeId)
    {
        if (_incomingEdges.TryGetValue(nodeId, out List<Edge>? edges))
        {
            return edges;
        }
        return default;
    }
}
