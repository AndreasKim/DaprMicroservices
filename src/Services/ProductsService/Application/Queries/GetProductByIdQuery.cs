using AutoMapper;
using Core.Application.Common.Interfaces;
using Core.Application.Common.Mappings;
using Core.Domain.Entities;
using MediatR;
using Services.ProductsService.Application.Specifications;

namespace Services.ProductsService.Application.Queries;

public record GetProductByIdQuery : IRequest<Product>, IMapTo<ProductFilter>
{
    public long Id { get; set; }
    public bool IncludeSalesInfo { get; set; }
    public bool IncludeRatings { get; set; }
}

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
