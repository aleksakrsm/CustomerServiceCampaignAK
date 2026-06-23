using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Guards
{
    public static class Guard
    {
        public static IGuardClause Against => GuardClause.Instance;
    }
}
