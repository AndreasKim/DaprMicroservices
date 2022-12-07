namespace Core.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int SalesInfoId { get; set; }
        public double RatingValue { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
    }
}   