using BotApi.Models;

namespace BotApi.Stores
{
    public class SessionStore
    {
        private readonly Dictionary<string, LoginSession> _sessions = new();

        public LoginSession CreateSession(string id)
        {
            var session = new LoginSession { SessionId = id };
            _sessions[id] = session;
            return session;
        }

        public LoginSession? Get(string id)
            => _sessions.TryGetValue(id, out var s) ? s : null;

        public void Complete(string id, long userId, string? username)
        {
            if (_sessions.TryGetValue(id, out var s))
            {
                s.TelegramUserId = userId;
                s.Username = username;
            }
        }
    }
}
