using Application.Dto;
using Application.Runtime;
using Application.Runtime.Workflow;
using GFlowApp.Services;
using Microsoft.AspNetCore.Mvc;
using Tmds.DBus.Protocol;

namespace MainApi.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly IWorkflowFactory _workflowFactory;
    private Dictionary<string, List<PropertySchema>> _cachedSchemas;
    public WorkflowController(IWorkflowFactory workflowFactory)
    {
        _workflowFactory = workflowFactory;
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
        if (_cachedSchemas.TryGetValue(nodeType, out var schema))
        {
            return Ok(schema);
        }
        var schema = nodeType switch
        {
            "sql" => new NodeTypeSchema(new List<PropertySchema>
            {
                "string",
                
            }),
            
        };
    } 
}
