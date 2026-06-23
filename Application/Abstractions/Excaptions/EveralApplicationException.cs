using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Excaptions
{
    public class EveralApplicationException : Exception
    {
        public EveralApplicationException(string message)
            : base(message)
        {
        }

        public EveralApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
