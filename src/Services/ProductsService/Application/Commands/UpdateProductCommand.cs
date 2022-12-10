using AutoMapper;
using Core.Application.Commands;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;

namespace Services.ProductsService.Application.Commands;

public record UpdateProductCommand : IRequest<Product>, IMapTo<Product>
{
    public long Id { get; set; }
    public int VendorId { get; set; }
    public string VendorName { get; set; }
    public string MainCategory { get; set; }
    public string SubCategory { get; set; }
    public bool Individualized { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public Uri Thumbnail { get; set; }
    public double Price { get; set; }
    public int SalesInfoId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

public class UpdateProductCommandHandler : UpdateCommandHandler<Product, UpdateProductCommand>
{
    public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}