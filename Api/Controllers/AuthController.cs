using Api.Models.ClientModels;
using Api.Models.Database;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/")]
public class AuthController(AuthService service) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<Account>> LogIn([FromBody] UserCredential credential)
    {
        try
        {
            return await service.LogIn(credential.Username, credential.Password);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}