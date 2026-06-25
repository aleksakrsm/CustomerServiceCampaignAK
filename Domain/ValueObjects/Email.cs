using Domain.Base;

namespace Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private Email()
        {
            Value = string.Empty;
        }

        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static Email Create(string email)
        {

            Guards.Guard.Against.NullOrWhiteSpace(email, "Email cannot be empty");

            var normalizedEmail = email.Trim().ToLowerInvariant();

            return new Email(normalizedEmail);
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
