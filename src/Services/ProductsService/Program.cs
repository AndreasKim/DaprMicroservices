using Core.Application;
using Core.Infrastructure;
using Google.Api;
using MediatR;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Services.ProductsService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5050, o => o.Protocols =
        HttpProtocols.Http2);
});

// Add services to the container.
builder.Services.AddApplication();
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddGrpc();
builder.Services.AddDaprClient();
builder.Services.AddControllers();
//builder.Services.AddHostedService<ProductsService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
