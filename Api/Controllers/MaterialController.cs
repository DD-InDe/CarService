using Api.Models.Dtos;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/materials")]
public class MaterialController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MaterialDto>> GetById([FromRoute] int id)
    {
        String action = "Просмотр материала по id";
        try
        {
            MaterialDto? material = await viewService.GetMaterialById(id);
            if (material == null)
            {
                logService.LogAction(action, false);
                return NotFound();
            }

            logService.LogAction(action, true);
            return Ok(material);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<MaterialDto>>> GetAll()
    {
        String action = "Просмотр материалов";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetMaterials());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}