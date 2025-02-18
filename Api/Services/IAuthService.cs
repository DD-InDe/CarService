using Api.Models.ClientModels;

namespace Api.Services;

public interface IAuthService
{
    Task<Account> LogIn(String login, String password);
}