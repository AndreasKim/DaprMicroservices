using Core.Application.Common.Interfaces;
using Core.Domain.Entities;
using MediatR;
using ProductsService.Specifications;

namespace ProductsService.Queries;

public record GetProductsQuery : IRequest<List<Product>>
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
    public GetProductsQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var filter = new ProductsFilter()
        {
            ActivePage = request.ActivePage,
            SubCategory = request.SubCategory,
            VendorId = request.VendorId,
            Amount = request.Amount,
            City = request.City,
            IsTrending = request.IsTrending,
            ProductsPerPage = request.ProductsPerPage,
        };

        return await _repository.ListAsync(new ProductsSpecification(filter), cancellationToken);
    }
}
