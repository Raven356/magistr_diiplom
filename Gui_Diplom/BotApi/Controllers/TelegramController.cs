using BotApi.Models.Requests;
using BotApi.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/telegram")]
    public class TelegramController : ControllerBase
    {
        private readonly TelegramService _telegram;

        public TelegramController(TelegramService telegram)
        {
            _telegram = telegram;
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Update update)
        {
            await _telegram.HandleUpdate(update);
            return Ok();
        }

        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationTelegram sendNotificationTelegram)
        {
            await _telegram.SendPhotoAsync(sendNotificationTelegram.TelegramUserId, sendNotificationTelegram.PhotoBytes);
            return Ok();
        }
    }
}
