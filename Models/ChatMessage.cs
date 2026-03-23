namespace IDFK.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string User { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int MovieId { get; set; } // מקשר לסרט הספציפי
        public DateTime Timestamp { get; set; } = DateTime.Now;

    }

}
