namespace Gui_Diplom.Models.ApiRequests
{
    internal class GetStatisticsByUserRequest
    {
        public Guid SessionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? UserId { get; set; }
    }
}
