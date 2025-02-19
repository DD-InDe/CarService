using Api.Models.ClientModels;
using Api.Models.ViewModels;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/")]
public class AccountController(AccountService accountService, LogService logService) : ControllerBase
{
    [HttpPost("authenticate")]
    public async Task<ActionResult<Account>> LogIn([FromBody] UserCredential credential)
    {
        String action = "Авторизация";
        try
        {
            Account? account = await accountService.LogIn(credential.Username, credential.Password);
            if (account == null)
            {
                logService.LogAction(action, false);
                return NotFound("Пользователь не найден!");
            }

            logService.LogAction(action, true);
            return Ok(account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }

    [HttpPost("registration")]
    public async Task<ActionResult> Registration([FromBody] EmployeeViewModel employee)
    {
        String action = "Регистрация";
        try
        {
            bool complete = await accountService.Registration(employee);
            if (complete)
            {
                logService.LogAction(action, true);
                Created();
            }

            logService.LogAction(action, false);
            return Conflict("Регистрация не прошла!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            logService.LogAction(action, false);
            return Conflict(e);
        }
    }
}