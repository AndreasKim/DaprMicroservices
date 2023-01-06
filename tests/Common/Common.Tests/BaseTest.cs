// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Reflection;
using Core.Application;
using Core.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Tests
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            var serviceCollection = InitializeServices();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }
        public IServiceProvider ServiceProvider { get; set; }
        public static AppSettings Settings => new AppSettings()
        {
            ConnectionStrings = new Connectionstrings()
            {
                DefaultConnection = "Server=(localdb)\\mssqllocaldb;Database=aspnet-DaprMicroservices-3EA58D71-FB94-4FBF-B643-2303120343;Trusted_Connection=True;MultipleActiveResultSets=true"
            }
        };

        public static IServiceCollection InitializeServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(new ActivitySource("Test"));
            return services;
        }

        public abstract void ConfigureServices(IServiceCollection services);
    }
}
