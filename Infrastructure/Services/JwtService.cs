using Application.Abstractions.Configurations;
using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class JwtService(IOptions<JwtOptions> jwtOptions) : IJwtService
    {
        static readonly int _expirationMinutes = 5;
        public AccessTokenResult GenerateAccessToken(User user)
        {

            var expiresAt = DateTime.UtcNow.AddMinutes(_expirationMinutes);

            SigningCredentials credentials;

            var secretKey = jwtOptions.Value.SecretKey;
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT SecretKey not configured");
            }

            ValidateSecretKey(secretKey);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
                Expires = expiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JsonWebTokenHandler();
            var tokenString = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessTokenResult(tokenString, expiresAt);
        }

        private static void ValidateSecretKey(string secretKey)
        {
            if (secretKey.Length < 32)
            {
                throw new InvalidOperationException($"JWT SecretKey is too weak");
            }

            var uniqueChars = secretKey.Distinct().Count();
            if (uniqueChars < 16)
            {
                throw new InvalidOperationException($"JWT SecretKey has low entropy (only {uniqueChars} unique characters). ");
            }
        }
    }
}
