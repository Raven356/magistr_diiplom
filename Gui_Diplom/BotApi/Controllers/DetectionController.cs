using BLL.Services;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/detection")]
    public class DetectionController
    {
        private readonly DetectionService detectionService;
        private readonly SessionService sessionService;
        private readonly VideoArchiveService videoArchiveService;

        public DetectionController(DetectionService detectionService, SessionService sessionService, VideoArchiveService videoArchiveService)
        {
            this.detectionService = detectionService;
            this.sessionService = sessionService;
            this.videoArchiveService = videoArchiveService;
        }

        [HttpPost("create")]
        public async Task<CreateDetectionResponse> CreateDetectionAsync(CreateDetectionRequest request)
        {
            var video = await videoArchiveService.GetVideoByFilePathAsync(request.VideoPath);

            var session = await sessionService.GetSessionByGuidAsync(request.SessionGuid);

            var detection = await detectionService.CreateDetection(new BLL.DbModels.Detection
            {
                DetectionDate = request.DetectionDate,
                SessionId = session.SessionId,
                VideoArchiveId = video.Id,
            });

            return new CreateDetectionResponse 
            {
                DetectionId = detection.Id,
            };
        }
    }
}
