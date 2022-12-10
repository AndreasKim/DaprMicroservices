using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

public record GetProductsQuery : IRequest<List<Product>>, IMapTo<ProductsFilter>
{
    public int Amount { get; set; }
    public bool IsTrending { get; set; }
    public string City { get; set; }
    public string SubCategory { get; set; }
    public long VendorId { get; set; }
    public int ActivePage { get; set; }
    public int ProductsPerPage { get; set; }
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<ProductsFilter>(request);

        return await _repository.ListAsync(new ProductsSpecification(filter), cancellationToken);
    }
}
