using AutoMapper;
using Core.Application.Commands;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record CreateProductCommand : IRequest<Product>, IMapTo<Product>
{
    public string? MainCategory { get; init; }
    public string? SubCategory { get; init; }
    public bool Individualized { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? Thumbnail { get; init; }
    public double Price { get; init; }
    public int SalesInfoId { get; init; }
}

public class CreateProductCommandHandler : CreateCommandHandler<Product, CreateProductCommand>
{
    public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}
