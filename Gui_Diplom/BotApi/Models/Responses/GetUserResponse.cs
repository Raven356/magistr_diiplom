namespace BotApi.Models.Responses
{
    public class GetUserResponse
    {
        public bool IsFound { get; set; }

        public int UserId { get; set; }

        public bool IsAdmin { get; set; }

        public string TelegramId { get; set; }
    }
}
