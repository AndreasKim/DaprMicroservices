using Core.Application.Common.Interfaces;
using Core.Domain.Entities;
using MediatR;
using ProductsService.Specifications;

namespace ProductsService.Queries;

public record GetProductByIdQuery : IRequest<Product>
{
    public long Id { get; set; }
    public bool IncludeSalesInfo { get; set; }
    public bool IncludeRatings { get; set; }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
{
    private readonly IRepository<Product> _repository;
    public GetProductByIdQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter()
        {
            Id = request.Id,
            IncludeSalesInfo = request.IncludeSalesInfo,
            IncludeRatings = request.IncludeRatings
        };

        return await _repository.SingleOrDefaultAsync(new ProductSpecification(filter), cancellationToken);
    }
}
