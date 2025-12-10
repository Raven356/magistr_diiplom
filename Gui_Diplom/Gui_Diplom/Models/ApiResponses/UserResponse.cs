namespace Gui_Diplom.Models.ApiResponses
{
    internal class UserResponse
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public int NotificationType { get; set; }

        public bool AuthenticatedToTelegram { get; set; }
    }
}
