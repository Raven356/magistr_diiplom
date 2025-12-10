namespace Gui_Diplom.Models.ApiResponses
{
    internal class CreateSessionResponse
    {
        public bool Success { get; set; }

        public Guid AccessToken { get; set; }
    }
}
