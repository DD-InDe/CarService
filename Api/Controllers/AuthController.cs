using Api.Models.ClientModels;
using Api.Models.Database;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthController(IJwtTokenManager jwtTokenManager, CarServiceDbContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult LogIn([FromBody] UserCredential credential)
    {
        try
        {
            return Ok(jwtTokenManager.Authenticate(credential.Username,credential.Password));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}