[ApiController]
[Route("api/requests")]
public class RequestsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestCommand cmd)
    {
        // Здесь будет вызов Application Layer
        string requestId = "req-" + Guid.NewGuid().ToString()[..8];
        return Created($"/api/requests/{requestId}", new { id = requestId });
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id) => Ok(new { id, status = "Active" });
}