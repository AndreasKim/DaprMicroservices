using Ardalis.Specification;
using Core.Domain.Entities;

namespace Services.ProductsService.Application.Specifications;

public class ProductFilter
{
    public long Id { get; set; }
    public bool IncludeSalesInfo { get; set; }
    public bool IncludeRatings { get; set; }
}

internal class ProductSpecification : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductSpecification(ProductFilter filter)
    {
        Query.Where(p => p.Id == filter.Id);

        if (filter.IncludeSalesInfo && filter.IncludeRatings)
            Query.Include(p => p.SalesInfo).ThenInclude(p => p.Ratings);

        if (filter.IncludeSalesInfo)
            Query.Include(p => p.SalesInfo);

    }

}

