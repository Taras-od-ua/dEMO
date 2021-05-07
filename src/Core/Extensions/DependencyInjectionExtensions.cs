
using Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Services;
using WebApi.Services;

namespace Core.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ICarService, CarService>();
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
