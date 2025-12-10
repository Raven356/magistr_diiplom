namespace Gui_Diplom.Models.ApiResponses
{
    internal class CreateUserResponse
    {
        public bool Success { get; set; }

        public int UserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
