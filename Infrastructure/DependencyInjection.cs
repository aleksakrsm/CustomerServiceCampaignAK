using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Persistance
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddHttpClient<ICustomerService, CustomerService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(15);
            });

            services.AddScoped<IEmailService, EmailServiceSimulation>();

            services.AddScoped<IJwtService, JwtService>();

            return services;

        }
    }
}
