namespace Gui_Diplom.Models.ApiRequests
{
    internal class CreateDetectionRequest
    {
        public Guid SessionGuid { get; set; }

        public DateTime DetectionDate { get; set; }

        public string VideoPath { get; set; }
    }
}
