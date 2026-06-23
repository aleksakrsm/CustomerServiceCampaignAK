using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Domain.Guards
{
    public interface IGuardClause
    {
        string NullOrWhiteSpace([NotNull] string value, string message);
        Guid Empty(Guid value, string message);
        decimal Negative(decimal value, string message);
        void InvalidCondition(bool condition, string message);
        string StringTooLong(string value, int maxLength, string message);
    }
}
