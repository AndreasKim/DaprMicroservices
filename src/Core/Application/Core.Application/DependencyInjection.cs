using AutoMapper;
using Core.Application.Commands;
using Core.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            MapperConfiguration mapperConfiguration = new MapperConfiguration(mapperConfig =>
            {
                mapperConfig.AddProfile<MappingProfile>();
            });
            services.AddSingleton(mapperConfiguration.CreateMapper());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
