using Api.Models.Database;
using Api.Models.Dtos;
using Api.Repositories;

namespace Api.Services;

public class AuthService(EmployeeRepository repository) : IAuthService
{
    public EmployeeDto LogIn(string login, string password)
    {
        try
        {
            return new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}