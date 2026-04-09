[ApiController]
[Route("api/requests")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _service;

    public RequestController(IRequestService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestCommand command)
    {
        var id = await _service.CreateRequestAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { requestId = id });
    }

    [HttpPost("{id}/assign-group")]
    public async Task<IActionResult> AssignGroup(string id, [FromBody] AssignGroupToRequestCommand command)
    {
        command = command with { RequestId = id };
        await _service.AssignGroupAsync(command);
        return NoContent();
    }

    [HttpPost("{id}/activate")]
    public async Task<IActionResult> Activate(string id, [FromBody] ActivateRequestCommand command)
    {
        command = command with { RequestId = id };
        await _service.ActivateRequestAsync(command);
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(string id, [FromBody] CompleteRequestCommand command)
    {
        command = command with { RequestId = id };
        await _service.CompleteRequestAsync(command);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RequestDto>> GetById(string id)
    {
        var query = new GetRequestByIdQuery { RequestId = id };
        var dto = await _service.GetRequestByIdAsync(query);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<List<RequestDto>>> GetActive([FromQuery] string? zoneId = null)
    {
        var query = new ListActiveRequestsQuery { ZoneId = zoneId };
        var list = await _service.ListActiveRequestsAsync(query);
        return Ok(list);
    }
}