// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Reflection;
using AutoFixture.Xunit2;
using Common.Tests;
using Core.Application;
using Core.Application.Models;
using Core.Infrastructure;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Services.ProductsService.Application.Commands;
using Services.ProductsService.Infrastructure.Persistence;
using Xunit;

namespace ProductsService.UnitTests
{
    public class CreateCommandTests : BaseTest
    {
        [Theory, AutoData]
        public async Task CreateProductCommand_CorrectData_ProductCreated(CreateProductCommand command)
        {
            var sender = ServiceProvider.GetService<ISender>().ThrowIfNull();
            command = command with { MainCategory = "Kunst" };
            var prod = await sender.Send(command);

            prod.Should().NotBeNull();
            prod.Id.Should().BePositive();
        }

        [Theory, AutoData]
        public async Task CreateProductCommand_InvalidData_ThrowsException(CreateProductCommand command)
        {
            var sender = ServiceProvider.GetService<ISender>().ThrowIfNull();

            Func<Task> action = async () => await sender.Send(command);
            await action.Should().ThrowAsync<AutoMapper.AutoMapperMappingException>();

        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var testAssembly = Assembly.GetAssembly(typeof(Services.ProductsService.ProductsService)).ThrowIfNull();

            services
                .AddApplication(testAssembly)
                .AddInfrastructure<ApplicationDbContext>(Settings, typeof(EfRepository<>));
        }
    }
}
