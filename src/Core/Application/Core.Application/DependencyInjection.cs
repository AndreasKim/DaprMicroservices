using AutoMapper;
using Core.Application.Commands;
using Core.Application.Helpers;
using Core.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), assembly);
            services.AddMediatR(Assembly.GetExecutingAssembly(), assembly);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            Protogen.Generate(assembly);

            return services;
        }
    }
}
