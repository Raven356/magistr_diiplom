namespace BotApi.Models.Requests
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string? Password { get; set; }

        public int NotificationType { get; set; }

        public string EmailAddress { get; set; }
    }
}
