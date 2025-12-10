namespace Gui_Diplom.Models.ApiResponses
{
    internal class LoginByPasswordResponse
    {
        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public int UserId { get; set; }

        public string TelegramId { get; set; }

        public bool IsAdmin { get; set; }
    }
}
