using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record GetProductsQuery : IRequest<List<Product>>, IMapTo<ProductsFilter>
{
    public int Amount { get; init; }
    public bool IsTrending { get; init; }
    public string? City { get; init; }
    public string? SubCategory { get; init; }
    public int ActivePage { get; init; }
    public int ProductsPerPage { get; init; }
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
