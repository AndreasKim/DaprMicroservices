using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using ProtoBuf;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public record GetProductByIdQuery(
    long Id,
    bool IncludeSalesInfo,
    bool IncludeRatings
) : IRequest<Product>, IMapTo<ProductFilter>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<ProductFilter>(request);

        return await _repository.SingleOrDefaultAsync(new ProductSpecification(filter), cancellationToken) ?? new Product();
    }
}
