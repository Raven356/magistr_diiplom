using BLL.Services;
using BotApi.Mappers;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/session")]
    public class SessionController
    {
        private readonly SessionService sessionService;

        public SessionController(SessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        [HttpPost("create")]
        public async Task<CreateSessionResponse> CreateSessionAsync(CreateSessionRequest createSessionRequest)
        {
            await sessionService.ExpireOldSessions(createSessionRequest.UserId);

            var result = await sessionService.CreateSessionAsync(SessionMapper.Map(createSessionRequest));

            return new CreateSessionResponse
            {
                Success = result != null,
                AccessToken = result != null ? result.AccessToken : Guid.Empty,
            };
        }
    }
}
