using Application.Dto;
using Application.Runtime;
using Application.Runtime.Workflow;
using GFlowApp.Services;
using GFlowApp.Services.Schemas;
using Microsoft.AspNetCore.Mvc;
using Tmds.DBus.Protocol;

namespace MainApi.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly IWorkflowFactory _workflowFactory;

    private readonly ISchemaRegister _schemasRegister;
    private Dictionary<string, NodeTypeSchema> _cachedSchemas;
    public WorkflowController(IWorkflowFactory workflowFactory, ISchemaRegister schemaRegister)
    {
        _workflowFactory = workflowFactory;
        _schemasRegister = schemaRegister;
        _cachedSchemas = new();

    }

    [HttpPost("execute")]
    public async Task<ActionResult<RuntimeResult>> ExecuteAsync([FromBody] WorkflowDto dto, CancellationToken cts)
    {
        var workflow = new Workflow(dto.Nodes, dto.Edges);
        if (workflow is null)
        {
            BadRequest(workflow);
        }
        var runtime = _workflowFactory.Create();

        var result = await runtime.RunAsync(workflow, cts);

        return Ok(result);
    }

//Todo перенсти схему из Services в API
    [HttpGet("nodes/schema/{nodeType}")]
    public ActionResult<NodeTypeSchema> GetSchema(string nodeType)
    {
        if (_cachedSchemas.TryGetValue(nodeType, out var cached))
        {
            return Ok(cached);
        }
        var parseNodeType = nodeType switch
        {
            string s when s.Contains("Action") => s.Replace("Action", "").Trim().ToLower(),
            string s when s.Contains("Logic") => s.Replace("Logic","").Trim().ToLower(),
            string s when s.Contains("Trigger") => s.Replace("Trigger", "").Trim().ToLower(),
            _ => throw new NotSupportedException($"not supported type {nodeType}") 
        };
        if (_schemasRegister.TryGet(parseNodeType, out var schema))
        {
            _cachedSchemas.Add(nodeType, schema!);
            return Ok(schema);

        }

        return StatusCode(500, new
        {
            Error = $"Не удалось получить схему для типа {nodeType}"
        });
    } 
}
