using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<string> GetCustomerByIdAsync(
            long customerId,
            CancellationToken cancellationToken = default);
    }
}
