using Application.Abstractions.Services.Models.CustomerService;

namespace Application.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByIdAsync(
            long customerId,
            CancellationToken cancellationToken = default);
    }
}
