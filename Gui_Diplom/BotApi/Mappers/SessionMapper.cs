using BLL.DbModels;
using BotApi.Models.Requests;

namespace BotApi.Mappers
{
    public static class SessionMapper
    {
        public static Session Map(CreateSessionRequest createSessionRequest)
        {
            return new Session
            {
                UserId = createSessionRequest.UserId,
                AccessToken = createSessionRequest.SessionId
            };
        }
    }
}
