using BLL.DbContexts;
using BLL.DbModels;

namespace BLL.Services
{
    public class DetectionService
    {
        private readonly FireDetectionContext context;

        public DetectionService(FireDetectionContext context)
        {
            this.context = context;
        }

        public async Task<Detection> CreateDetection(Detection detection)
        {
            var result = await context.Detections.AddAsync(detection);
            await context.SaveChangesAsync();

            return result.Entity;
        }
    }
}
