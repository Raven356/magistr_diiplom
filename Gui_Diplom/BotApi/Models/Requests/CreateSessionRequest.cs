namespace BotApi.Models.Requests
{
    public class CreateSessionRequest
    {
        public Guid SessionId { get; set; }

        public int UserId { get; set; }
    }
}
