using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Application.Executors.Action.Http;

public class HttpRequestHandler : IRequestHandler
{
    private readonly HttpClient _client;

    public HttpRequestHandler(HttpClient client) => _client = client;
    public async Task<HttpResponseMessage?> HandleRequest(HttpDefinition defenition)
    {
        HttpResponseMessage? response = defenition.MethodType switch
        {
            HttpMethodType.GET => await HandleGetMethod(defenition),
            HttpMethodType.POST => await HandlePostMethod(defenition)
        };

        return response;
    }

    private async Task<HttpResponseMessage?> HandleGetMethod(HttpDefinition defenition)
    {
        try
        {
            var url = defenition.GetArgument<string>(HttpRequestArgument.Url.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            UseAuthorization(request, defenition.Auth);
            var response = await _client.GetAsync(url);
            if (response is null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"Ресурс не найден или ответ не содержит информации")
                };
            }
            return await HandleResponse(response);
        }
        catch (HttpRequestException httpEx)
        {
            var statusCode = httpEx.StatusCode ?? System.Net.HttpStatusCode.InternalServerError;

            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent($"Возникла ошибка во время выполнения запроса на получение : {httpEx.Message}")
            };
        }
        catch (OperationCanceledException opEx)
        {
            throw;
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Ошибка сервера: {ex.Message}")
            };
        }
    }
    private async Task<HttpResponseMessage?> HandlePostMethod(HttpDefinition defenition)
    {
        try
        {
            var url = defenition.GetArgument<string>(HttpRequestArgument.Url.ToString());

            var content = ContentTypeFactory.Create(defenition);

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            UseAuthorization(request, defenition.Auth);
            if (content is not null)
            {
                request.Content = content;
            }

            var response = await _client.SendAsync(request);

            return await HandleResponse(response);

            
        }catch (HttpRequestException httpEx)
        {
            var statusCode = httpEx.StatusCode ?? System.Net.HttpStatusCode.InternalServerError;

            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent($"Возникла ошибка во время выполнения запроса на отправку данных : {httpEx.Message}")
            };
        }
        catch (OperationCanceledException opEx)
        {
            throw;
        }
        catch (Exception ex)
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Ошибка сервера: {ex.Message}")
            };
        }
    }
    
    private async Task<HttpResponseMessage?> HandleResponse(HttpResponseMessage? responseMessage)
    {
        if (responseMessage is not null)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var errors = await responseMessage.Content.ReadAsStringAsync();
                return new HttpResponseMessage(responseMessage.StatusCode)
                {
                    Content = new StringContent($"Ошибка запроса к внешнему источнику. Код {(int)responseMessage.StatusCode}")
                };
            }

            return responseMessage;
        }

        return null;
    }
    private void UseAuthorization(HttpRequestMessage? request, AuthDefinition? auth)
    {
        if (request is null || auth is null || auth.AuthType is AuthType.None)
        {
            return;
        }

        switch (auth.AuthType)
        {
            case AuthType.Bearer:
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.Token);
                break;
            case AuthType.Basic:
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{auth.Username}:{auth.Password}")
                );
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                break;
            case AuthType.Cookie:
                if (!string.IsNullOrEmpty(auth.CookieString))
                {
                    request.Headers.Add("Cookie", auth.CookieString);
                }
                break;
        }

    }
    

}
