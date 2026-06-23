using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Base
{
    public abstract record AddCommand : Command<Guid>
    {
        public Guid Id { get; init; } = Guid.NewGuid();
    }
}
