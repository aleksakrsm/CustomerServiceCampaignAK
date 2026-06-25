using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface ICurrentUserService
    {
        CurrentUser GetCurrentUser();

        Guid GetUserId();
        UserRole GetUserRole();
        bool IsAuthenticated();
    }

    public class CurrentUser
    {
        public static readonly CurrentUser Anonymous = new()
        {
            Id = Guid.Empty,
            Email = "anonymous@system",
            Role = UserRole.Regular,
            IsAnonymous = true,
        };

        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public UserRole Role { get; set; }

        public bool IsAnonymous { get; init; }

        public bool HasRole(UserRole requiredRole)
        {
            return Role >= requiredRole;
        }

        public bool IsAdmin => Role == UserRole.Admin;

    }
}
