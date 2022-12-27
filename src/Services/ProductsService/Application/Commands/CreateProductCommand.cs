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
    public string? MainCategory { get; set; }
    public string? SubCategory { get; set; }
    public bool Individualized { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public double Price { get; set; }
    public int SalesInfoId { get; set; }
}

public class CreateProductCommandHandler : CreateCommandHandler<Product, CreateProductCommand>
{
    public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}
