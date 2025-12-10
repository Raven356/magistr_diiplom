namespace Gui_Diplom.Models.ApiRequests
{
    internal class CreateNotificationRequest
    {
        public int DetectionId { get; set; }

        public int NotificationSource { get; set; }

        public DateTime NotificationDate { get; set; }
    }
}
