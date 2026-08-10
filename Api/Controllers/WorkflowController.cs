using Application.Dto;
using Application.Runtime;
using Application.Runtime.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class WorkflowController : ControllerBase
{
    private readonly IWorkflowFactory _workflowFactory;
    public WorkflowController(IWorkflowFactory workflowFactory)
    {
        _workflowFactory = workflowFactory;

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
        
        var result =  await runtime.RunAsync(workflow, cts);

        return Ok(result);
    }
}
