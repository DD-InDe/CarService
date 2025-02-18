using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("api/test")]
public class Test : ControllerBase
{
    [HttpGet("sayHello")]
    public ActionResult<String> SayHello()
    {
        return "hello";
    }
}