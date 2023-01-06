using System.Reflection;
using Core.Application;
using Core.Application.Models;
using Core.Infrastructure;
using Services.ProductsService;
using Services.ProductsService.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddKestrel()
    .AddAppSettings(out var settings)
    .AddCustomSerilog()
    .AddOpenTelemetry();

// Add services to the container.
builder.Services
    .AddApplication(Assembly.GetExecutingAssembly())
    .AddInfrastructure<ApplicationDbContext>(settings, typeof(EfRepository<>))
    .AddServiceDependencies(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ProductsService>();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
