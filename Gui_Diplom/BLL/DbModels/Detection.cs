using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DbModels
{
    public class Detection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SessionId { get; set; }

        public DateTime DetectionDate { get; set; }

        public int VideoArchiveId { get; set; }

        public VideoArchive VideoArchive { get; set; }

        public Session Session { get; set; }

        public List<Notification> Notification { get; set; }
    }
}
