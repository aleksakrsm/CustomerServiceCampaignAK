using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        static readonly int _expirationMinutes = 5;
        public AccessTokenResult GenerateAccessToken(User user)
        {

            var expiresAt = DateTime.UtcNow.AddMinutes(_expirationMinutes);

            var claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = user.Id.ToString(),
                [JwtRegisteredClaimNames.Email] = user.Email.Value,
                [ClaimTypes.NameIdentifier] = user.Id.ToString(),
                [ClaimTypes.Email] = user.Email.Value,
                [ClaimTypes.Role] = user.Role.ToString()
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = expiresAt
            };

            var tokenHandler = new JsonWebTokenHandler();
            var tokenString = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessTokenResult(tokenString, expiresAt);
        }
    }
}
