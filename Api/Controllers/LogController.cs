using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/log")]
public class LogController(LogService logService) : ControllerBase
{
    [Authorize]
    [HttpGet("logs")]
    public ActionResult<List<LogModel>> GetLogs()
    {
        try
        {
            return Ok(logService.GetLogs());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}