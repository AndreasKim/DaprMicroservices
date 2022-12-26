using Core.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject1
{

    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            var services = new ServiceCollection();
            services.AddApplication();

            var serviceProvider = services.BuildServiceProvider();

            var sender = serviceProvider.GetService<ISender>();
            //var result = sender.Send(new CreateProductCommand()).Result;
        }
    }
}