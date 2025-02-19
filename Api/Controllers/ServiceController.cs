using Api.Models.Dtos;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ServiceDto>> GetById([FromRoute] int id)
    {
        String action = "Просмотр услуги по id";
        try
        {
            ServiceDto? serviceDto = await viewService.GetServiceById(id);
            if (serviceDto == null)
            {
                logService.LogAction(action, false);
                return NotFound();
            }

            logService.LogAction(action, true);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ServiceDto>>> GetAll()
    {
        String action = "Просмотр услуг";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetServices());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}