using Api.Models.Dtos;
using Api.Services.ModelServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/employees/")]
public class EmployeeController(EmployeeService employeeService) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EmployeeDto>> GetById([FromRoute] int id)
    {
        try
        {
            EmployeeDto? employee = await employeeService.GetObjectById(id);
            return employee != null ? Ok(employee) : NotFound();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }

    [HttpGet]
    public async Task<ActionResult<EmployeeDto>> GetAll()
    {
        try
        {
            return Ok(await employeeService.GetAllObjects());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}