using Microsoft.IdentityModel.Tokens;
using OnlinePlatformWeb.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShoppingCartService.Services;

public class TokenService(IConfiguration _configuration) : ITokenService
{
    public string GenerateToken(string userId, string username)
    {
        var secretKey = _configuration["Jwt:Secret"] ?? "ShoppingCartServiceProjectDanielaDimitrovska!";//todo, this should be from key vault
        var issuer = _configuration["Jwt:Issuer"] ?? "ShoppingCartService";
        var audience = _configuration["Jwt:Audience"] ?? "ShoppingCartAPI";

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, userId),
            new Claim(ClaimTypes.NameIdentifier, userId),    
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(ClaimTypes.Name, username),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}