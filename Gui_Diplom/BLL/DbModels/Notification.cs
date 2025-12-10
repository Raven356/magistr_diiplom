using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DbModels
{
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int DetectionId { get; set; }

        public int NotificationSource { get; set; }

        public DateTime NotificationDate { get; set; }

        public Detection Detection { get; set; }
    }
}
