using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Ineval.Dto
{
    public static class RequestClient
    {
        public static async Task<string> PostItem(string url, string json)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                string res = responseString;
                return res;
            }
            catch (HttpRequestException)
            {
                return "";
            }
            finally
            {
                response.Content.Dispose();
            }
        }

        public static async Task<string> GetItem(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                string res = responseString;
                return res;
            }
            catch (HttpRequestException)
            {
                return "";
            }
            finally
            {
                response.Content.Dispose();
            }
        }

        public static async Task<string> GetPostItem(string url)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent("", UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                string res = responseString;
                return res;
            }
            catch (HttpRequestException)
            {
                return "";
            }
            finally
            {
                response.Content.Dispose();
            }
        }

        public class ResponseCliente
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}