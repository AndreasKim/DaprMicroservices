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
    public int Id { get; set; }
    public string? MainCategory { get; set; }
    public string? SubCategory { get; set; }
    public bool Individualized { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public double Price { get; set; }
    public int SalesInfoId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductCommand, Product>()
            .ForMember(src => src.Thumbnail, opt => opt.MapFrom(dest => new Uri(dest.Thumbnail)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

public class UpdateProductCommandHandler : UpdateCommandHandler<Product, UpdateProductCommand>
{
    public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}