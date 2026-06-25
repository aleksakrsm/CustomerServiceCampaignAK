using Domain.Entities;

namespace Application.Abstractions.Services
{
    public sealed record AccessTokenResult(string Token, DateTime ExpiresAt);
    public interface IJwtService
    {
        AccessTokenResult GenerateAccessToken(User user);
    }
}
