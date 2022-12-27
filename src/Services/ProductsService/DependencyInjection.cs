using Man.Dapr.Sidekick;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDaprClient();
            services.AddDaprSidekick(configuration, p => p.Sidecar = 
                new DaprSidecarOptions() { AppProtocol = "grpc", AppId = "productsservice", ComponentsDirectory = "..\\..\\..\\dapr\\components" });

            services.AddGrpc();
            services.AddGrpcReflection();

            return services;
        }
    }
}
