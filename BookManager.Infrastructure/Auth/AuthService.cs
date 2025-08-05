using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookManager.Infrastructure.Auth;

public class AuthService : IAuthService
{
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private readonly IConfiguration _configuration;
    public string ComputeHash(string password)
    {
        using (var hash = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            
            var hashBytes = hash.ComputeHash(passwordBytes);
            
            var builder = new StringBuilder();

            foreach (var t in hashBytes)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
        );
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("username", email),
            new Claim(ClaimTypes.Role, role),
        };
        
        var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(2), credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}