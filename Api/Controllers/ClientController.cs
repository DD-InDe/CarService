using Api.Models.Dtos;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController(ClientService clientService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById([FromRoute] int id)
    {
        try
        {
            ClientDto? client = await clientService.GetObjectById(id);
            return client != null ? Ok(client) : NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        try
        {
            return Ok(await clientService.GetAllObjects());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}