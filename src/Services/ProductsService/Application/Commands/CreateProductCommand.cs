using AutoMapper;
using Core.Application.Commands;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record CreateProductCommand(    
    int VendorId,
    string VendorName,
    string MainCategory,
    string SubCategory,
    bool Individualized,
    string Name,
    string Description,
    string Thumbnail,
    double Price,
    int SalesInfoId
) : IRequest<Product>, IMapTo<Product>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductCommand, Product>()
            .ForMember(src => src.CreationDate, opt => opt.MapFrom(dest => DateTime.Now))
            .ForMember(src => src.Thumbnail, opt => opt.MapFrom(dest => new Uri(dest.Thumbnail)));
    }
}

public class CreateProductCommandHandler : CreateCommandHandler<Product, CreateProductCommand>
{
    public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}
