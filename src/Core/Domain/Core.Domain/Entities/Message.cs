namespace Core.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int ConversationId { get; set; } 
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }

        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
