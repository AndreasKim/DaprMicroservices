using Core.Application.Models;

namespace Core.Application.Helpers
{
    public static class PaginationHelper
    {
        public static int CalculateSkip(BaseFilter filter) => filter.ProductsPerPage * filter.ActivePage;
        public static int CalculateTake(BaseFilter filter) => filter.ProductsPerPage;

    }
}
