using BLL.DbContexts;
using BLL.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class VideoArchiveService
    {
        private readonly FireDetectionContext fireDetectionContext;

        public VideoArchiveService(FireDetectionContext fireDetectionContext)
        {
            this.fireDetectionContext = fireDetectionContext;
        }

        public async Task<VideoArchive> CreateOrUpdateVideoArchiveAsync(VideoArchive video)
        {
            var existingVideo = await fireDetectionContext.VideoArchives.FirstOrDefaultAsync(v => v.FilePath == video.FilePath);

            if (existingVideo != null) 
            {
                existingVideo.AccessTime = video.AccessTime;
            }
            else
            {
                var entity = await fireDetectionContext.VideoArchives.AddAsync(video);

                existingVideo = entity.Entity;
            }

            await fireDetectionContext.SaveChangesAsync();

            return existingVideo;
        }

        public async Task<VideoArchive> GetVideoByFilePathAsync(string path)
        {
            var video = await fireDetectionContext.VideoArchives.FirstOrDefaultAsync(v => v.FilePath == path);

            return video;
        }
    }
}
