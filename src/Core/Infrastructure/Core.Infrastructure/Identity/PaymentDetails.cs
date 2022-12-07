namespace Core.Infrastructure.Identity
{
    public class PaymentDetails
    {
        public long Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CVV { get; set; }
        public DateTime ValidTill { get; set; } 
    }
}
