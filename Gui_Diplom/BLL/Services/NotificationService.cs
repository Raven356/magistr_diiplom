using BLL.DbContexts;
using BLL.DbModels;

namespace BLL.Services
{
    public class NotificationService
    {
        private readonly FireDetectionContext context;

        public NotificationService(FireDetectionContext context)
        {
            this.context = context;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            var result = await context.Notifications.AddAsync(notification);

            await context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
