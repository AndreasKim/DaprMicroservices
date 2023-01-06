using Core.Application.Interfaces;
using Core.Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure<T>(this IServiceCollection services, AppSettings settings, Type repository) where T : DbContext
        {
            var connectionString = settings.ConnectionStrings?.DefaultConnection
                ?? throw new ArgumentNullException(nameof(settings), "Default connection is not set in the app settings.");

            services.AddDbContext<T>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(T).Assembly.FullName)));

            services.AddScoped(typeof(IRepository<>), repository);
            //services.AddScoped<ApplicationDbContextInitialiser>();

            return services;
        }
    }
}
