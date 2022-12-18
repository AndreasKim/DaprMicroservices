using AutoMapper;
using Core.Application.Commands;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;

namespace Services.ProductsService.Application.Commands;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record UpdateProductCommand(
    long Id,
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
        profile.CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(src => src.Thumbnail, opt => opt.MapFrom(dest => new Uri(dest.Thumbnail)))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

public class UpdateProductCommandHandler : UpdateCommandHandler<Product, UpdateProductCommand>
{
    public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}