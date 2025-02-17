using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class JwtTokenManager(IConfiguration configuration) : IJwtTokenManager
{
    public string Authenticate(string username, string password)
    {
        String key = configuration.GetValue<String>("JwtConfig:Key")!;
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, username)]),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256)
        };
        SecurityToken token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}