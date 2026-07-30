namespace Application.Exceptions;

public class NotEqualTypeException : Exception
{
    public string? Message { get; }

    public NotEqualTypeException(string? message) => Message = message;
}