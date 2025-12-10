using BLL.DbModels;
using BotApi.Models.Requests;

namespace BotApi.Mappers
{
    public static class NotificationMapper
    {
        public static Notification Map(CreateNotificationRequest request)
        {
            return new Notification
            {
                DetectionId = request.DetectionId,
                NotificationDate = request.NotificationDate,
                NotificationSource = request.NotificationSource,
            };
        }
    }
}
