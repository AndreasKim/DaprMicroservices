using Core.Application;
using Core.Infrastructure;
using Man.Dapr.Sidekick;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Services.ProductsService;
using Services.ProductsService.Infrastructure.Persistence;
using Services.ProductsService.Protos;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5050, o => o.Protocols =
        HttpProtocols.Http2);
});

// Add services to the container.
builder.Services
    .AddApplication(Assembly.GetExecutingAssembly())
    .AddInfrastructure<ApplicationDbContext>(builder.Configuration, typeof(EfRepository<>))
    //.AddAutoMapper(Assembly.GetExecutingAssembly())
    .AddMediatR(typeof(Program))
    .AddEndpointsApiExplorer()
.AddControllers();

builder.Services.AddDaprClient();
builder.Services.AddDaprSidekick(builder.Configuration, p => p.Sidecar = new DaprSidecarOptions() { AppProtocol = "grpc", AppId = "productsservice" });
builder.Services.AddGrpc();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Protogen.Generate();
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
