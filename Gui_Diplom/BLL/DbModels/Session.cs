using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DbModels
{
    public class Session
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        public Guid AccessToken { get; set; }

        public int UserId { get; set; }

        public bool IsExpired { get; set; }

        public User User { get; set; }

        public List<Detection> Detections { get; set; }
    }
}
