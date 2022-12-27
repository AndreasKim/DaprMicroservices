using AutoMapper;
using Core.Application.Commands;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record UpdateProductCommand : IRequest<Product>, IMapTo<Product>
{
    public int Id { get; init; }
    public string? MainCategory { get; init; }
    public string? SubCategory { get; init; }
    public bool Individualized { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? Thumbnail { get; init; }
    public double Price { get; init; }
    public int SalesInfoId { get; init; }
}

public class UpdateProductCommandHandler : UpdateCommandHandler<Product, UpdateProductCommand>
{
    public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}