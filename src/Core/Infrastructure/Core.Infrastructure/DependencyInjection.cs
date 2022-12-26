using Core.Application.Interfaces;
using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<T>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString, 
                builder => builder.MigrationsAssembly(typeof(T).Assembly.FullName)));

            //services.AddScoped<ApplicationDbContextInitialiser>();


            return services;
        }
    }
}
