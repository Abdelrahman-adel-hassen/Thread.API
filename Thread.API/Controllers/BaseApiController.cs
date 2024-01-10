namespace Thread.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(LogUserActivity))]
public class BaseApiController : ControllerBase
{
}
