namespace Core.Domain.Entities
{
    public class Conversation
    {
        public Conversation()
        {  }

        public Conversation(string userId, Product product)
        {
            BuyerId = userId;
            Product = product;
            SellerId = product.Vendor.ShopOwnerId;
        }

        public int Id { get; set; } 
        public string BuyerId { get; set; }
        public string SellerId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

    }
}
