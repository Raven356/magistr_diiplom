using RestSharp;
using RestSharp.Authenticators;

namespace Gui_Diplom.Helpers
{
    public static class MailgunHelper
    {
        public static async Task<bool> TriggerMailgunNotification(byte[] annotatedBytes)
        {
#if DEBUG
            return true;
#else
            var options = new RestClientOptions("https://api.mailgun.net")
            {
                Authenticator = new HttpBasicAuthenticator("api", "key")
            };

            var client = new RestClient(options);
            var request = new RestRequest("/v3/sandboxf42a751c479042138efd0fc39059dfbc.mailgun.org/messages", Method.Post)
            {
                AlwaysMultipartFormData = true
            };

            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxf42a751c479042138efd0fc39059dfbc.mailgun.org>");
            request.AddParameter("to", "Alex Raven <matroskin7777@gmail.com>");
            request.AddParameter("subject", "Fire Detected!");
            request.AddParameter("text", "Fire was detected. See attached image.");
            request.AddFile("attachment", annotatedBytes, "annotated.jpg", "image/jpeg");

            var response = await client.ExecuteAsync(request);

            return response.IsSuccessStatusCode;
#endif
        }
    }
}
