namespace Domain.Graph;

public sealed record Edge(int FromNode, int ToNode, string FromPort, string ToPort);
