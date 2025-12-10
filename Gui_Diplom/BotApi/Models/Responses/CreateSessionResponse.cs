namespace BotApi.Models.Responses
{
    public class CreateSessionResponse
    {
        public bool Success { get; set; }

        public Guid AccessToken { get; set; }
    }
}
