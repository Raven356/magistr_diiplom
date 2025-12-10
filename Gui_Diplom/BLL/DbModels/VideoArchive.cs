using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DbModels
{
    public class VideoArchive
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FilePath { get; set; }

        public DateTime AccessTime { get; set; }

        public List<Detection> Detections { get; set; }
    }
}
