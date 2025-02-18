using Api.Models.ClientModels;
using Api.Models.Database;
using Api.Models.Dtos;
using Api.Models.ViewModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/")]
public class PersonController(AccountService accountService, LogService logService) : ControllerBase
{
    [HttpPost("authenticate")]
    public async Task<ActionResult<Account>> LogIn([FromBody] UserCredential credential)
    {
        try
        {
            Account? account = await accountService.LogIn(credential.Username, credential.Password);
            if (account == null)
            {
                logService.LogAction("Авторизация", false);
                return NotFound("Пользователь не найден!");
            }

            logService.LogAction("Авторизация", true);
            return Ok(account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction("Авторизация", false);
            return Conflict(e);
        }
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody] EmployeeViewModel employee)
    {
        try
        {
            return await accountService.Registration(employee) ? Created() : Conflict("Регистрация не прошла!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}