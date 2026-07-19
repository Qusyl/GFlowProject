namespace Domain.Nodes
{
    public sealed record NodePropertyDescriptor(
        string PropertyName,
        Type PropertyType,
        object? DefaultValue,
        bool IsRequired
    );
    
}
