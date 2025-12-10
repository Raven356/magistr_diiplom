using BLL.Services;
using BotApi.Helpers;
using BotApi.Mappers;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService) 
        {
            this.userService = userService;
        }

        [HttpPost("createUser")]
        public async Task<CreateUserResponse> CreateUserAsync([FromBody] CreateUserRequest createUserRequest)
        {
            var existingUser = await userService.GetUserByNameAsync(createUserRequest.UserName);

            if (existingUser != null)
            {
                return new CreateUserResponse
                {
                    Success = false,
                    ErrorMessage = "Username already exists!",
                    UserId = -1
                };
            }

            var user = await userService.CreateUserAsync(UserMapper.Map(createUserRequest));

            return new CreateUserResponse
            {
                Success = user != null,
                UserId = user != null ? user.Id : -1
            };
        }

        [HttpGet("getByTelegramId/{telegramUserId}")]
        public async Task<GetUserResponse> GetUserByTelegramUserIdAsync(string telegramUserId)
        {
            var user = await userService.GetUserByTelegramUserIdAsync(telegramUserId);

            return new GetUserResponse
            {
                IsFound = user != null,
                UserId = user != null ? user.Id : -1,
                IsAdmin = user != null && user.IsAdmin,
                TelegramId = telegramUserId
            };
        }

        [HttpGet("getByUserName/{username}")]
        public async Task<GetUserResponse> GetUserByUserNameAsync(string username)
        {
            var user = await userService.GetUserByNameAsync(username);

            return new GetUserResponse
            {
                IsFound = user != null,
                UserId = user != null ? user.Id : -1,
                IsAdmin = user != null && user.IsAdmin,
                TelegramId = user?.TelegramId
            };
        }

        [HttpGet("getByGoogleUserId/{googleUserId}")]
        public async Task<GetUserResponse> GetUserByGoogleUserIdAsync(string googleUserId)
        {
            var user = await userService.GetUserByGoogleUserIdAsync(googleUserId);

            return new GetUserResponse
            {
                IsFound = user != null,
                UserId = user != null ? user.Id : -1,
                IsAdmin = user != null && user.IsAdmin,
                TelegramId = user?.TelegramId
            };
        }

        [HttpGet("getBySessionGuid/{sessionId}")]
        public async Task<UserResponse> GetUserBySessionId(Guid sessionId)
        {
            var user = await userService.GetUserBySessionId(sessionId);

            return new UserResponse
            {
                Id = user.Id,
                NotificationType = user.NotificatonType,
                UserEmail = user.EmailAddress,
                UserName = user.UserName,
                AuthenticatedToTelegram = !string.IsNullOrEmpty(user.TelegramId)
            };
        }

        [HttpPost("update")]
        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            await userService.UpdateUserAsync(new BLL.DbModels.User
            {
                UserName = request.UserName,
                EmailAddress = request.EmailAddress,
                NotificatonType = request.NotificationType,
                Password = HashHelper.Hash(request.Password),
                Id = request.Id,
            });
        }

        [HttpPost("update/telegram")]
        public async Task UpdateTelegramUserId(UpdateUserTelegramRequest request)
        {
            await userService.UpdateUserTelegramId(request.UserId, request.TelegramId);
        }

        [HttpGet("all")]
        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await userService.GetAllUsers();

            var response = new List<UserResponse>();

            foreach (var user in users) 
            {
                response.Add(new UserResponse
                {
                    Id = user.Id,
                    AuthenticatedToTelegram = !string.IsNullOrEmpty(user.TelegramId),
                    NotificationType = user.NotificatonType,
                    UserEmail = user.EmailAddress,
                    UserName = user.UserName,
                });
            }

            return response;
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteById(int id)
        {
            await userService.DeleteById(id);
        }
    }
}
