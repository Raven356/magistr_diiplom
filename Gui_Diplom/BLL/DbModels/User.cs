using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DbModels
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string UserName { get; set; }

        public string? Password { get; set; }

        public string? GoogleUserId { get; set; }

        public int AuthType { get; set; }

        public string? TelegramId { get; set; }

        public bool IsAdmin { get; set; }

        public string EmailAddress { get; set; }

        public int NotificatonType { get; set; }

        public List<Session> Sessions { get; set; }
    }
}
