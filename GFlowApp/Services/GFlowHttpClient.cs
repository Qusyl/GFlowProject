
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Application.Dto;
using Application.Runtime;

namespace GFlowApp.Services
{
    public class GFlowHttpClient : IClientService
    {
        private readonly HttpClient _client;

        public GFlowHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<RuntimeResult?> ExecuteAsync(WorkflowDto dto)
        {

            var response = await _client.PostAsJsonAsync("/api/workflow/execute", dto);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<RuntimeResult>();
         }

        public async Task<NodeTypeSchema?> GetSchemaAsync(string NodeType)
        {
            var response = await _client.GetFromJsonAsync<NodeTypeSchema?>($"/api/workflow/nodes/schema/{NodeType}");

            return response;
        }
    }
}