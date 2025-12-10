namespace Gui_Diplom.Models.ApiRequests
{
    internal class SendNotificationTelegram
    {
        public long TelegramUserId { get; set; }

        public byte[] PhotoBytes { get; set; }
    }
}
