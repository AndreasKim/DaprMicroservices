// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoFixture.Xunit2;
using Common.Tests;
using Core.Domain.Entities;
using Core.Infrastructure;
using Dapr.Client;
using FluentAssertions;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.DependencyInjection;
using Services.ProductsService.Generated;
using Services.ProductsService.Infrastructure.Persistence;

namespace ProductsService.E2ETests
{

    public class CrudTests : BaseTest
    {
        [Theory, AutoData]
        public async Task CreateProduct(Product product)
        {
            var request = new CreateProductRequest()
            {
                Description = product.Description,
                Individualized = product.Individualized,
                MainCategory = product.MainCategory.ToString(),
                Name = product.Name,
                Price = product.Price,
                SalesInfoId = product.SalesInfoId,
                SubCategory = product.SubCategory,
                Thumbnail = product.Thumbnail?.ToString()
            };

            await RunTest(async p =>
            {
                var productId = await p.InvokeMethodGrpcAsync<CreateProductRequest, Int32Value>
                    ("productsservice", "createproduct", request, new CancellationToken());

                productId.Should().NotBeNull();
                productId.Value.Should().BePositive();
            });
        }

        private static async Task RunTest(Action<DaprClient> testAction)
        {
            var ts = new CancellationTokenSource();
            var task = Task.Run(() =>
            {
                Task.Run(() => ts.Token.ThrowIfCancellationRequested());
                var entryPoint = typeof(Program).Assembly.EntryPoint!;
                entryPoint.Invoke(null, new object[] { Array.Empty<string>() });
            }, ts.Token);

            await Task.Delay(3000);
            var client = new DaprClientBuilder().Build();

            testAction(client);
            await Task.Delay(3000);
            ts.Cancel();
            ts.Dispose();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure<ApplicationDbContext>(Settings, typeof(EfRepository<>));
        }
    }
}
