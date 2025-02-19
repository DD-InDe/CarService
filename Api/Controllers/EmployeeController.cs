using Api.Models.Dtos;
using Api.Services;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/employees/")]
public class EmployeeController(ViewService viewService, LogService logService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetById([FromRoute] int id)
    {
        String action = "Просмотр сотрудника по id";
        try
        {
            logService.LogAction(action, true);
            EmployeeDto? employee = await viewService.GetEmployeeById(id);
            return employee != null ? Ok(employee) : NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<EmployeeDto>> GetAll()
    {
        String action = "Просмотр сотрудников";
        try
        {
            logService.LogAction(action, true);
            return Ok(await viewService.GetEmployees());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}