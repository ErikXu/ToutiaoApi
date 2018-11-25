using Newtonsoft.Json;

namespace ToutiaoApi.Requests
{
    public class AuthRequest
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; } = "auth_code";

        [JsonProperty("auth_code")]
        public string AuthCode { get; set; }
    }
}