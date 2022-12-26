using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using ProtoBuf;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public class GetProductByIdDto : IResponse, IMapFrom<Product>
{
    public int Id { get; set; }
    public string? MainCategory { get; set; }
    public string? SubCategory { get; set; }
    public bool Individualized { get; set; } = true;
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile
            .CreateMap<Product, GetProductByIdDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
    }
}   
