namespace Application.Exceptions;

public class NullNodePropertyReferenceException : Exception
{
    public string? Message { get; init; }

    public NullNodePropertyReferenceException(string? message) => Message = message;
}