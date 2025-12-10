using BLL.DbModels;
using BotApi.Stores;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BotApi.Services
{
    public class TelegramService
    {
        private readonly ITelegramBotClient _bot;
        private readonly SessionStore _sessions;
        string _botToken = "token";

        public TelegramService(SessionStore sessions)
        {
            _bot = new TelegramBotClient(_botToken);
            _sessions = sessions;
        }

        public async Task HandleUpdate(Update update)
        {
            if (update.Type != UpdateType.Message || update.Message!.Text == null)
                return;

            string text = update.Message.Text;
            long userId = update.Message.From!.Id;
            string? username = update.Message.From.Username;

            // We expect: /start <sessionId>
            if (text.StartsWith("/start "))
            {
                string sessionId = text.Substring(7).Trim();

                _sessions.Complete(sessionId, userId, username);

                await _bot.SendMessage(update.Message.Chat.Id,
                    $"🔥 Login confirmed.");
            }
        }

        public async Task SendPhotoAsync(long telegramUserId, byte[] photoBytes)
        {
            using var stream = new MemoryStream(photoBytes);

            await _bot.SendPhoto(
                chatId: telegramUserId,
                photo: new InputFileStream(new MemoryStream(photoBytes)),
                caption: "🔥 Fire detected!"
            );
        }
    }
}
