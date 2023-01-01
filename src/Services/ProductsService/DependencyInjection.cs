using Man.Dapr.Sidekick;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Services.ProductsService;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static readonly string AppId = nameof(ProductsService).ToLower();
        public const string ServiceVersion = "1.0.0";

        public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDaprClient();
            services.AddDaprSidekick(configuration, p => p.Sidecar = 
                new DaprSidecarOptions() { AppProtocol = "grpc", AppId = AppId, 
                    ComponentsDirectory = "..\\..\\..\\dapr\\components", ConfigFile= "..\\..\\..\\dapr"});

            services.AddGrpc();
            services.AddGrpcReflection();

            services.AddSingleton(new ActivitySource(AppId));

            return services;
        }

        public static WebApplicationBuilder AddCustomSerilog(this WebApplicationBuilder builder)
        {
            var config = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .WriteTo.Console()
                .Enrich.FromLogContext()          
                .Enrich.WithProperty("ApplicationId", AppId);

            var seqServerUrl = builder.Configuration["SeqServerUrl"];
            if(!string.IsNullOrWhiteSpace(seqServerUrl))
            {
                config = config.WriteTo.Seq(seqServerUrl);
            }

            Log.Logger = config.CreateLogger();

            builder.Host.UseSerilog();
            return builder;
        }

        public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder)
        {
            var meter = new Meter(AppId);
            var counter = meter.CreateCounter<long>("app.request-counter");
            var appResourceBuilder = ResourceBuilder.CreateDefault()
                .AddService(serviceName: AppId, serviceVersion: ServiceVersion);

            var hasZipkinUrl = Uri.TryCreate(builder.Configuration["ZipkinServerUrl"], new UriCreationOptions(), out var zipkinServerUrl);

            builder.Services.AddOpenTelemetry()
                .WithTracing(tracerProviderBuilder =>
                {
                    tracerProviderBuilder
                        .AddSource(AppId)
                        .SetResourceBuilder(
                            ResourceBuilder.CreateDefault()
                                .AddService(serviceName: AppId, serviceVersion: ServiceVersion))
                        .AddGrpcClientInstrumentation()
                        .AddAspNetCoreInstrumentation()
                        .AddEntityFrameworkCoreInstrumentation();

                    if (hasZipkinUrl)
                    {
                        tracerProviderBuilder
                            .AddZipkinExporter(p => p.Endpoint = zipkinServerUrl);
                    }
                })
                .WithMetrics(metricProviderBuilder =>
                {
                    metricProviderBuilder
                        .AddMeter(meter.Name)
                        .SetResourceBuilder(appResourceBuilder)
                        .AddAspNetCoreInstrumentation();
                }).StartWithHost();

            return builder;
        }

        public static WebApplicationBuilder AddKestrel(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                // Setup a HTTP/2 endpoint without TLS.
                options.ListenLocalhost(5050, o => o.Protocols =
                    HttpProtocols.Http2);
            });
            return builder;
        }
    }
}
