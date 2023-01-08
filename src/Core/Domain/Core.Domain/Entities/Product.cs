using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public MainCategory MainCategory { get; set; }
        public string? SubCategory { get; set; }
        public bool Individualized { get; set; } = true;
        public required string Name { get; set; }
        public string? Description { get; set; }
        public Uri? Thumbnail { get; set; }
        public double Price { get; set; }
        public int SalesInfoId { get; set; }
        public virtual SalesInfo SalesInfo { get; set; } = new SalesInfo();
    }

}
