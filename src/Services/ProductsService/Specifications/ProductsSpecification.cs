using Ardalis.Specification;
using Core.Domain.Entities;
using Core.Application.Common.Helpers;
using Core.Application.Common.Models;

namespace ProductsService.Specifications;

public class ProductsFilter : BaseFilter
{
    public int Amount { get; set; }
    public bool IsTrending { get; set; }
    public string City { get; set; }
    public string SubCategory { get; set; }
    public long VendorId { get; set; }
}

internal class ProductsSpecification : Specification<Product>
{
    public ProductsSpecification(ProductsFilter filter)
    {
        if (filter.IsTrending)
            Query.OrderBy(p => p.CreationDate).ThenBy(p => p.SalesInfo.NumberOfSales);

        if (!string.IsNullOrWhiteSpace(filter.City))
            Query.Where(p => p.Vendor.City == filter.City);

        if (!string.IsNullOrWhiteSpace(filter.SubCategory))
            Query.Where(p => p.SubCategory == filter.SubCategory);

        if (filter.VendorId > 0)
            Query.Where(p => p.VendorId == filter.VendorId);


        if (filter.Amount > 0)
            Query.Take(filter.Amount);
        else
        {
            Query.Skip(PaginationHelper.CalculateSkip(filter))
                 .Take(PaginationHelper.CalculateTake(filter));
        }
    }

}

