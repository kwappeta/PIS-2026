[ApiController]
[Route("api/groups")]
public class GroupsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok(new[] { new { id = "g1", name = "Поисковая группа Альфа" } });

    [HttpPost]
    public IActionResult Create([FromBody] object group) => Ok(new { id = "g-new" });
}