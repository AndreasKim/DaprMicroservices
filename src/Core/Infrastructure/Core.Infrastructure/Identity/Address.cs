using System.ComponentModel.DataAnnotations;

namespace Core.Infrastructure.Identity
{
    public class Address
    {
        public long Id { get; set; }
        public string Street { get; set; }

        public string StreetNo { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PLZ { get; set; }

        [Phone]
        public string Telephone { get; set; }

        [EmailAddress]
        public string CorporateEmail { get; set; }

    }
}
