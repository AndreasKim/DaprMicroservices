using AutoMapper;
using Core.Application.Commands;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record DeleteProductCommand : IRequest<Product>, IMapTo<Product>
{   
    public int Id { get; init; }
}

public class DeleteProductCommandHandler : DeleteCommandHandler<Product, DeleteProductCommand>
{
    public DeleteProductCommandHandler(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}