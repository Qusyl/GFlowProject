

namespace Application.Executors.Action.Http
{
    public sealed record AuthDefinition(
        AuthType AuthType,
        string? Username,
        string? Password,
        string? Token,
        string? CookieString
    )
    {
        
    }
}