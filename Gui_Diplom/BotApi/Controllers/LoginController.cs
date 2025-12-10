using BLL.Services;
using BotApi.Helpers;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using BotApi.Stores;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly SessionStore _sessions;
        private readonly UserService userService;

        public LoginController(SessionStore sessions, UserService userService)
        {
            _sessions = sessions;
            this.userService = userService;
        }

        [HttpGet("{sessionId}")]
        public IActionResult Check(string sessionId)
        {
            var session = _sessions.Get(sessionId);

            if (session == null)
                return NotFound();

            return Ok(new
            {
                session.IsCompleted,
                session.TelegramUserId,
                session.Username
            });
        }

        [HttpPost("create")]
        public IActionResult Create()
        {
            string id = Guid.NewGuid().ToString("N");
            _sessions.CreateSession(id);

            return Ok(new { sessionId = id });
        }

        [HttpPost("loginPassword")]
        public async Task<LoginByPasswordResponse> LoginByPasswordAsync(LoginByPasswordRequest loginByPasswordRequest)
        {
            var user = await userService.GetUserByNameAsync(loginByPasswordRequest.UserName);

            bool isValid = HashHelper.VerifyHash(loginByPasswordRequest.Password, user?.Password);

            return new LoginByPasswordResponse
            {
                IsSuccess = isValid,
                Error = isValid ? string.Empty : (user == null ? "User was not found!" :
                    (string.IsNullOrEmpty(user?.Password) ? "User doesn't have password, as login was done in different method!" 
                        : "Password was not verified!")),
                UserId = user == null ? -1 : user.Id,
                TelegramId = user?.TelegramId,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
