using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class BusinessRuleValidationException(string message) : DomainException(message)
    {
    }
}
