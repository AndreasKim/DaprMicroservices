using Core.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common.Helpers
{
    public static class PaginationHelper
    {
        public static int CalculateSkip(BaseFilter filter) => filter.ProductsPerPage * filter.ActivePage;
        public static int CalculateTake(BaseFilter filter) => filter.ProductsPerPage;

    }
}
