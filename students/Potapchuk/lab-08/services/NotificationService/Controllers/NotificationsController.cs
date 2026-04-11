[ApiController]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { message = "Уведомления работают" });
}