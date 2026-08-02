namespace Application.Executors.Action.Http;

public interface IRequestHandler
{
    Task<HttpResponseMessage?> HandleRequest(HttpDefinition defenition);
}