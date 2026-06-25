using Domain.Base;
using Domain.Guards;

namespace Domain.Entities
{
    public class User : AggregateRoot
    {
        public ValueObjects.Email Email { get; private set; } = null!;
        public UserRole Role { get; private set; }
        private User()
        {
        }
        public static User Create(
            string email,
            UserRole role,
            Guid? agentId = null)
        {
            Guard.Against.NullOrWhiteSpace(email, "Email is required");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = ValueObjects.Email.Create(email),
                Role = role,
            };

            return user;
        }

    }

    public enum UserRole
    {
        Regular = 0,
        Admin = 10,
    }
}
