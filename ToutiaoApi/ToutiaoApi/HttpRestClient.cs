using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToutiaoApi
{
    public interface IHttpRestClient
    {
        Task<T> GetAsync<T>(string url, string accessToken);

        Task<T> PostAsync<T>(string url, object request, string accessToken = null);
    }

    public class HttpRestClient : IHttpRestClient
    {
        public async Task<T> GetAsync<T>(string url, string accessToken)
        {
            using (var client = GetClient(accessToken))
            {
                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        public async Task<T> PostAsync<T>(string url, object request, string accessToken = null)
        {
            using (var client = GetClient(accessToken))
            {
                using (var response = await client.PostAsync(url, Serialize(request)))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        private static HttpClient GetClient(string accessToken = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Add("Access-Token", accessToken);
            }
            
            return client;
        }

        private static HttpContent Serialize(object data)
        {
            if (data == null)
            {
                return null;
            }

            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static T Deserialize<T>(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                var serializer = new JsonSerializer();
                return (T)serializer.Deserialize(reader, typeof(T));
            }
        }
    }
}