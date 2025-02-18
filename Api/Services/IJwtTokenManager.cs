namespace Api.Services;

public interface IJwtTokenManager
{
    String Authenticate(String username);
}