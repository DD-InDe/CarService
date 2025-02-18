using Api.Models.ClientModels;
using Api.Models.Dtos;
using Api.Models.ViewModels;
using Api.Services.ModelServices;

namespace Api.Services;

public class AccountService(IJwtTokenManager jwtTokenManager, EmployeeService service) : IAccountService
{
    public async Task<Account?> LogIn(string login, string password)
    {
        try
        {
            EmployeeDto? dto = await service.GetObjectByData(login, password);
            return dto != null
                ? new() { Employee = dto, Token = jwtTokenManager.Authenticate(login) }
                : null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<bool> Registration(EmployeeViewModel viewModel)
    {
        try
        {
            return await service.AddObject(viewModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}