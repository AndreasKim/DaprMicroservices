using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class Vendor
    {
        public Vendor(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string ShopOwnerId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        [Range(0,5)]
        public double Rating { get; set; }
        public int NumberOfSales { get; set; }
        public double TotalAmountSales { get; set; }
        public string Description { get; set; }
        public Uri Logo { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PLZ { get; set; }

        [Phone]
        public string Telephone { get; set; }

        [EmailAddress]
        public string CorporateEmail { get; set; }

        public string AGB { get; set; }
        public string Shipment { get; set; }
        public string Revocation { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
