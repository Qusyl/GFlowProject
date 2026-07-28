using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.ExeptionHandler;

public class WorkflowExceptionHandler : IExceptionHandler
{
    private readonly ILogger<WorkflowExceptionHandler> _logger;

    public WorkflowExceptionHandler(ILogger<WorkflowExceptionHandler> logger){ _logger = logger; }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError("Произошла ошибка : {message}", exception.Message);

        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Ресурс не найден"),
            _ => (StatusCodes.Status500InternalServerError, "Ошибка сервера")
        };
        httpContext.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
