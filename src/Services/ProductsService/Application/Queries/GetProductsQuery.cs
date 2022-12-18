using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record GetProductsQuery(
    int Amount,
    bool IsTrending,
    string City,
    string SubCategory,
    long VendorId,
    int ActivePage,
    int ProductsPerPage
) : IRequest<List<Product>>, IMapTo<ProductsFilter>;

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
