using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/materials")]
public class MaterialController : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}