using Application.Context;
using Application.Exceptions;
using Application.Executors.Configurations;
using Application.Executors.Logic.Logics;

namespace Application.Executors.Action.Http.Request;

[NodeExecutor("HttpRequest")]
public class ActionHttpRequestExecutor : INodeExecutors
{
    private readonly HttpClient _client;
    public ActionHttpRequestExecutor(HttpClient client) => _client = client;
    public async Task<NodeResult> ExecuteAsync(NodeExecutionContext context)
    {
        var httpDefiniton = context.Node.Node.GetProperty<HttpDefinition>("HttpContext");

        if (httpDefiniton is null)
        {
            return NodeResult.Failure(new NullNodePropertyReferenceException($"Can't get property {typeof(HttpDefinition)} (http request)"));
        }
        try
        {
            var handler = new HttpRequestHandler(_client);

            var response = await handler.HandleRequest(httpDefiniton);

            context.WorkflowContext.SetVariable("HttpResponse",
            new VariableValue(
                    new VariableDefenition("Response",
                    ArgumentType.HttpResponseMessage), response
            ));

            return NodeResult.Success(context.Node.Descriptor.GetOutcomingPorts());
        }
        catch (Exception ex)
        {
            return NodeResult.Failure(ex);
        }

    }
}
