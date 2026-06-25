using Application.Abstractions.Configurations;
using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;


namespace Persistance
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {

            services.AddHttpClient<ICustomerService, CustomerService>(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(15);
            });

            services.AddScoped<IEmailService, EmailServiceSimulation>();

            services.AddScoped<IJwtService, JwtService>();

            services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .ValidateOnStart();

            return services;

        }
    }
}
