
using System;
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
            try
            {
                var response = await _client.PostAsJsonAsync("/v1/Workflow/execute", dto);

                response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RuntimeResult>();
            }catch(Exception ex)
            {
                System.Console.WriteLine($"HttpClientError: {ex.Message}");
                throw;
            }
         }

        public async Task<NodeTypeSchema?> GetSchemaAsync(string NodeType)
        {
            try
            {
                 var response = await _client.GetFromJsonAsync<NodeTypeSchema?>($"/v1/Workflow/nodes/schema/{NodeType}");

            return response;
            }catch(Exception ex)
            {
                System.Console.WriteLine($"HttpClientError: {ex.Message}");
                throw;
            }
        }
    }
}