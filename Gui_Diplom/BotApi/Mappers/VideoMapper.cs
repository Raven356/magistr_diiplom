using BLL.DbModels;
using BotApi.Models.Requests;

namespace BotApi.Mappers
{
    public static class VideoMapper
    {
        public static VideoArchive Map(CreateVideoRequest createVideoRequest)
        {
            return new VideoArchive
            {
                AccessTime = createVideoRequest.AccessTime,
                FilePath = createVideoRequest.VideoPath
            };
        }
    }
}
