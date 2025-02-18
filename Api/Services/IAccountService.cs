using Api.Models.ClientModels;
using Api.Models.Dtos;
using Api.Models.ViewModels;

namespace Api.Services;

public interface IAccountService
{
    Task<Account?> LogIn(String login, String password);
    Task<bool> Registration(EmployeeViewModel employee);
}