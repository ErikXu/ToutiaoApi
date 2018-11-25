using Newtonsoft.Json;

namespace ToutiaoApi.Responses
{
    public class AuthResponse : Response<AuthData>
    {

    }

    public class AuthData
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("advertiser_id")]
        public long AdvertiserId { get; set; }

        [JsonProperty("refresh_token_expires_in")]
        public int RefreshTokenExpiresIn { get; set; }
    }
}