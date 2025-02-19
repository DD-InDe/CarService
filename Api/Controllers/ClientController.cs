using Api.Models.Dtos;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById([FromRoute] int id)
    {
        String action = "Просмотр клиента по id";
        try
        {
            logService.LogAction(action, true);

            ClientDto? client = await viewService.GetClientById(id);
            return client != null ? Ok(client) : NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        String action = "Просмотр всех клиентов";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetClients());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}