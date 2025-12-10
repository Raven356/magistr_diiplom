using BLL.Services;
using BotApi.Mappers;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/video")]
    public class VideoArchiveController
    {
        private readonly VideoArchiveService videoArchiveService;

        public VideoArchiveController(VideoArchiveService videoArchiveService)
        {
            this.videoArchiveService = videoArchiveService;
        }

        [HttpPost("createOrUpdateVideo")]
        public async Task<CreateVideoResponse> CreateOrUpdateVideoAsync(CreateVideoRequest createVideoRequest)
        {
            try
            {
                await videoArchiveService.CreateOrUpdateVideoArchiveAsync(VideoMapper.Map(createVideoRequest));
            }
            catch (Exception ex) 
            {
                return new CreateVideoResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }

            return new CreateVideoResponse 
            {
                Success = true,
            };
        }
    }
}
