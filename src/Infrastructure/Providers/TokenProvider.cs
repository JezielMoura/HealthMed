using System.Security.Claims;
using HealthMed.Application.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace HealthMed.Infrastructure.Providers;

internal sealed class TokenProvider(byte[] key) : ITokenProvider
{
    private readonly byte[] _key = key;

    public async Task<string> Create(string name, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([ new("name", name), new("role", role) ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(securityToken);

        return await Task.FromResult(jwtToken);
    }
}
