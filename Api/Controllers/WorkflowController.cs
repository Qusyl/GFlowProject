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

    var result = await runtime.RunAsync(workflow, cts);

        Console.WriteLine($" JSON__WORKFLOW__RESULT: |{result.IsSuccess}| , |{result.Exception?.Message ?? "None"}| ");

        return StatusCode(200, new
        {
            Success = result.IsSuccess,
            Message = result.Exception?.Message ?? "None"
        });
    }
}
