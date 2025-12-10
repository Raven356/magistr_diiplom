namespace BotApi.Models.Requests
{
    public class SendNotificationTelegram
    {
        public long TelegramUserId { get; set; }

        public byte[] PhotoBytes { get; set; }
    }
}
