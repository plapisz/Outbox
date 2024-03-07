using Microsoft.AspNetCore.Mvc;

namespace Outbox.Samples.Api.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => Ok("Outbox samples Api");
}
