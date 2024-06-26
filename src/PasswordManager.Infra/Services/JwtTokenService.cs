using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using PasswordManager.Application.Services;
using PasswordManager.Domain.Entities;
using PasswordManager.Infra.Configurations;

namespace PasswordManager.Infra.Services;

public class JwtTokenService(IOptions<JwtSettings> jwtSettings) : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateToken(User user)
    {
        SigningCredentials signingCredentials =
            new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!)),
                SecurityAlgorithms.HmacSha256
            );

        Claim[] claims =
        [
            new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        JwtSecurityToken securityToken =
            new(
                _jwtSettings.Issuer,
                claims: claims,
                audience: _jwtSettings.Audience,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpiryMinutes)
            );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}