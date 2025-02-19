using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/")]
public class LogController(LogService logService) : ControllerBase
{
    [Authorize]
    [HttpGet("logs")]
    public ActionResult<List<LogModel>> GetLogs()
    {
        try
        {
            List<LogModel> logs = logService.GetLogs();
            logService.LogAction("Просмотр логов", true);
            return Ok(logs);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction("Просмотр логов", false);
            return Conflict(e);
        }
    }
}