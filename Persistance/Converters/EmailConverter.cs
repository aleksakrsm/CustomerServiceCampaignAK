using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistance.Converters
{
    public sealed class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter()
            : base(
                email => email.Value, // to provider (database)
                value => Email.Create(value)) // from provider
        {
        }
    }
}
