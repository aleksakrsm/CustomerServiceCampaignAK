using Application.Mapping.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(
                cfg =>
                {
                    // optional extra config
                },
                typeof(MappingProfile).Assembly);

            return services;

        }
    }
}
