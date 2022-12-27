using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using ProtoBuf;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public class GetProductByIdDto : IResponse, IMapFrom<Product>
{
    public int Id { get; init; }
    public string? MainCategory { get; init; }
    public string? SubCategory { get; init; }
    public bool Individualized { get; init; } = true;
    public string? Name { get; init; }
    public string? Description { get; init; }
    public double Price { get; init; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<Product, GetProductByIdDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
    }
}   
