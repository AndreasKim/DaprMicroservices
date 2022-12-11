using Dapr.Client;
using DaprMicroservices.Shared;
using GrpcServiceSample.Generated;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.ProductsService.Application.Commands;
using System.Threading;

namespace DaprMicroservices.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ISender sender;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISender sender)
        {
            _logger = logger;
            this.sender = sender;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            using var client = new DaprClientBuilder().Build();
            var deposit = new GrpcServiceSample.Generated.GetAccountRequest() { Id = "17"};
            var account = client.InvokeMethodGrpcAsync<GrpcServiceSample.Generated.GetAccountRequest, Account>("products", "getaccount", deposit, new CancellationToken()).Result;
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}