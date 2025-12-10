namespace Gui_Diplom.Models.ApiRequests
{
    internal class CreateUserRequest
    {
        public required string UserName { get; set; }

        public string? Password { get; set; }

        public string? GoogleUserId { get; set; }

        public int AuthType { get; set; }

        public string? TelegramId { get; set; }

        public string? EmailAddress { get; set; }

        public int NotificatonType { get; set; }
    }
}
