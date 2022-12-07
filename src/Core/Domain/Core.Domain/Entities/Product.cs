namespace Core.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        public string VendorName { get; set; }
        public MainCategory MainCategory { get; set; }
        public string SubCategory { get; set; }
        public bool Individualized { get; set; } = true;
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }  
        public Uri Thumbnail { get; set; }
        public double Price { get; set; }
        public int SalesInfoId { get; set; }
        public virtual SalesInfo SalesInfo { get; set; } = new SalesInfo();
    }

}
