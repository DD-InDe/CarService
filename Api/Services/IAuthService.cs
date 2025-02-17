using Api.Models.Dtos;

namespace Api.Services;

public interface IAuthService
{
    EmployeeDto LogIn(String login, String password);
}