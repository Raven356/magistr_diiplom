namespace BotApi.Models.Requests
{
    public class CreateDetectionRequest
    {
        public Guid SessionGuid { get; set; }

        public DateTime DetectionDate { get; set; }

        public string VideoPath { get; set; }
    }
}
