using Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Interceptors;

namespace Persistance
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<SoftDeleteInterceptor>();

            services.AddDbContext<MyDbContext>(
                    (serviceProvider, options) =>
                    {

                        options.UseSqlServer(
                            "Server=(localdb)\\MSSQLLocalDB;Database=campaigndb;Trusted_Connection=True;TrustServerCertificate=True;",
                            sqlOptions =>
                            {
                                sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
                            });

                        options.AddInterceptors(serviceProvider.GetRequiredService<SoftDeleteInterceptor>());
                    });

            services.AddScoped<IDbContext>(sp => sp.GetRequiredService<MyDbContext>());

            return services;

        }
    }
}
