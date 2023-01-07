// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using AutoFixture.Xunit2;
using AutoMapper;
using Common.Tests;
using Core.Application;
using Core.Application.Interfaces;
using Core.Application.Models;
using Core.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Services.ProductsService.Application.Commands;
using Xunit;

namespace ProductsService.UnitTests
{
    public class CreateCommandTests : BaseTest
    {

        [Theory, AutoMockData]
        public async Task CreateProductCommand_CorrectData_ProductCreated(CreateProductCommand command,
            IRepository<Product> repository, [Frozen] Mock<IMapper> mapper)
        {
            mapper.Setup(p => p.Map<Product>(It.IsAny<CreateProductCommand>()))
                .Returns(new Product() { Id = 1 });

            var handler = new CreateProductCommandHandler(repository, mapper.Object);
            command = command with { MainCategory = "Kunst" };
            var prod = await handler.Handle(command, default);

            prod.Should().NotBeNull();
            prod.Id.Should().BePositive();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var testAssembly = Assembly.GetAssembly(typeof(Services.ProductsService.ProductsService)).ThrowIfNull();

            services
                .AddApplication(testAssembly);
        }
    }
}
