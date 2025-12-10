namespace Gui_Diplom.Models.ApiRequests
{
    internal class CreateSessionRequest
    {
        public Guid SessionId { get; set; }

        public int UserId { get; set; }
    }
}
