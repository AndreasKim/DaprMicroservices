using MediatR;
using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using Core.Application.Commands;

namespace Services.ProductsService.Application.Commands;


public record CreateProductCommand : IRequest<Product>, IMapTo<Product>
{
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
        profile.CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now));
    }
}

public class CreateProductCommandHandler : CreateCommandHandler<Product, CreateProductCommand>
{
    public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
        : base(repository, mapper) { }
}
