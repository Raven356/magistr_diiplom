namespace BotApi.Models.Responses
{
    public class CreateUserResponse
    {
        public bool Success { get; set; }

        public int UserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
