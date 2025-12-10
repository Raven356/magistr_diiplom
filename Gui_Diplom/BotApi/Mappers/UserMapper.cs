using BLL.DbModels;
using BotApi.Helpers;
using BotApi.Models.Requests;

namespace BotApi.Mappers
{
    public static class UserMapper
    {
        public static User Map(CreateUserRequest createUserRequest)
        {
            return new User
            {
                UserName = createUserRequest.UserName,
                AuthType = createUserRequest.AuthType,
                GoogleUserId = createUserRequest.GoogleUserId,
                Password = HashHelper.Hash(createUserRequest.Password),
                TelegramId = createUserRequest.TelegramId,
                IsAdmin = false,
                NotificatonType = createUserRequest.NotificatonType,
                EmailAddress = createUserRequest.EmailAddress == null ? string.Empty : createUserRequest.EmailAddress,
            };
        }
    }
}
