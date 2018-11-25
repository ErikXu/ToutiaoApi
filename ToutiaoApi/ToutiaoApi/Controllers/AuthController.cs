using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using ToutiaoApi.Requests;
using ToutiaoApi.Responses;

namespace ToutiaoApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : ControllerBase
    {
        private readonly IHttpRestClient _restClient;
        private readonly IMemoryCache _cache;
        private readonly ToutiaoSetting _setting;

        public AuthController(IHttpRestClient restClient, IMemoryCache cache, IOptions<ToutiaoSetting> setting)
        {
            _restClient = restClient;
            _setting = setting.Value;
            _cache = cache;
        }

        [HttpGet]
        public IActionResult Redirect()
        {
            var authUrl = "http://ad.toutiao.com/openapi/audit/oauth.html";
            var redirectUrl = $"{Request.Scheme}://{Request.Host}/api/auth/callback";
            var url = $"{authUrl}?app_id={_setting.AppId}&state=&scope=[\"ad_service\",\"report_service\",\"dmp_service\",\"account_service\"]&redirect_uri={redirectUrl}";
            return Redirect(url);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback([FromQuery(Name = "auth_code")]string authCode)
        {
            var url = "https://ad.toutiao.com/open_api/oauth2/access_token/";

            var request = new AuthRequest
            {
                AppId = _setting.AppId,
                Secret = _setting.Secret,
                AuthCode = authCode
            };

            var response = await _restClient.PostAsync<AuthResponse>(url, request);
            response.EnsureSuccess();

            _cache.Set(ToutiaoCacheKey.AccessToken, response.Data.AccessToken);
            return Redirect("/swagger/index.html");
        }
    }
}