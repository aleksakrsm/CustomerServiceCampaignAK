using Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Guards
{
    internal sealed class GuardClause : IGuardClause
    {
        public static readonly GuardClause Instance = new();

        private GuardClause()
        {
        }
        public string NullOrWhiteSpace([NotNull] string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new BusinessRuleValidationException(message);
            }

            return value;
        }

        public Guid Empty(Guid value, string message)
        {
            if (value == Guid.Empty)
            {
                throw new BusinessRuleValidationException(message);
            }

            return value;
        }

        public decimal Negative(decimal value, string message)
        {
            if (value < 0)
            {
                throw new BusinessRuleValidationException(message);
            }

            return value;
        }


        public void InvalidCondition(bool condition, string message)
        {
            if (!condition)
            {
                throw new BusinessRuleValidationException(message);
            }
        }

        public string StringTooLong(string value, int maxLength, string message)
        {
            if (value.Length > maxLength)
            {
                throw new BusinessRuleValidationException(message);
            }

            return value;
        }
    }
}
