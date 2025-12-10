using BLL.Services;
using BotApi.Mappers;
using BotApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController
    {
        private readonly NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost("create")]
        public async Task CreateNotificationAsync(CreateNotificationRequest request)
        {
            await notificationService.CreateNotificationAsync(NotificationMapper.Map(request));
        }
    }
}
