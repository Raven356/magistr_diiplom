namespace BotApi.Models
{
    public class LoginSession
    {
        public string SessionId { get; set; }

        public long? TelegramUserId { get; set; }

        public string? Username { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public bool IsCompleted => TelegramUserId != null;
    }
}
