using Application.Dto;
using Application.Runtime.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace MainApi.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class WorkflowController : ControllerBase
{
    public WorkflowController() { }

    [HttpPost("execute")]
    public async Task<ActionResult> ExecuteAsync([FromBody] WorkflowDto dto, CancellationToken cts)
    {
        var workflow = new Workflow(dto.Nodes, dto.Edges);
    if (workflow is null)
    {
            return UnprocessableEntity(workflow);
    }

        
        var runtime = new WorkflowRuntime();
        

       await runtime.RunAsync(workflow, cts);

        

        return StatusCode(200, new
        {
          
        });
    }
}
