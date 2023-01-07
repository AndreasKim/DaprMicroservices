// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Core.Domain.Entities;
using Services.ProductsService.Application.Commands;
using Services.ProductsService.Application.Queries;
using Services.ProductsService.Generated;

namespace Services.ProductsService.Protos;

public class GrpcProfile : Profile
{
    public GrpcProfile()
    {
        CreateMap<GetProductByIdRequest, GetProductByIdQuery>();
        CreateMap<GetProductsRequest, GetProductsQuery>();
        CreateMap<CreateProductRequest, CreateProductCommand>();
        CreateMap<DeleteProductRequest, DeleteProductCommand>();
        CreateMap<PerformanceTestRequest, PerformanceTestCommand>();
        CreateMap<UpdateProductRequest, UpdateProductCommand>();
        CreateMap<PerformanceTestDto, PerformanceTestResponse>();
        CreateMap<GetProductByIdDto, GetProductByIdResponse>();
    }
}
