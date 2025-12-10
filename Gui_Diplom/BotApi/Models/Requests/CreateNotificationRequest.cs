namespace BotApi.Models.Requests
{
    public class CreateNotificationRequest
    {
        public int DetectionId { get; set; }

        public int NotificationSource { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
