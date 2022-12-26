using AutoMapper;
using Core.Application;
using Core.Application.Mappings;
using Core.Infrastructure;
using Google.Api;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

// Add services to the container.
builder.Services.AddApplication();

builder.Services.AddControllersWithViews(); 
//builder.Services.AddMediatR(typeof(Services.ProductsService.ProductsService));
//builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
