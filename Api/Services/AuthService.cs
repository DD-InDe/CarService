using Api.Models.ClientModels;
using Api.Models.Dtos;
using Api.Repositories;
using Api.Services.ModelServices;

namespace Api.Services;

public class AuthService(IJwtTokenManager jwtTokenManager, EmployeeService service) : IAuthService
{
    public async Task<Account> LogIn(string login, string password)
    {
        try
        {
            EmployeeDto dto = await service.GetObjectByData(login, password);
            return new Account()
            {
                Employee = dto,
                Token = jwtTokenManager.Authenticate(login)
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}