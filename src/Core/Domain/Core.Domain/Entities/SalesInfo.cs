using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
    public class SalesInfo
    {
        public int Id { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        [NotMapped]
        public int[] RatingScore { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double AverageRating => RatingScore.Select((p, i) => (double) p * (i + 1)).Sum() / RatingScore.Sum();

        public int NumberOfSales { get; set; }  
    }
}
