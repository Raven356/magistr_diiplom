using System.Text;

namespace Gui_Diplom.Helpers
{
    public static class HttpHelper<T>
    {
        public static async Task<dynamic> PostAsync(string url)
        {
            using HttpClient http = new();
            var res = await http.PostAsync(url, null);

            var json = await res.Content.ReadAsStringAsync();
            dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            return data;
        }

        public static async Task<T> PostAsync(string url, object body)
        {
            using HttpClient http = new();

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var res = await http.PostAsync(url, content);

            string responseJson = await res.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseJson);
        }

        public static async Task PostWithoutResponseAsync(string url, object body)
        {
            using HttpClient http = new();

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);

            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            await http.PostAsync(url, content);
        }

        public static async Task<T> GetAsync(string url)
        {
            using HttpClient http = new();
            var res = await http.GetAsync(url);

            var json = await res.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        
        public static async Task DeleteWithoutResponseAsync(string url)
        {
            using HttpClient http = new();

            await http.DeleteAsync(url);
        }
    }
}
