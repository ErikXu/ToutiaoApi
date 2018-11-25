using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ToutiaoApi.Responses;

namespace ToutiaoApi.Controllers
{
    /// <summary>
    /// 广告主API
    /// </summary>
    [Route("api/advertisers")]
    [ApiController]
    public class AdvertisersController : ControllerBase
    {
        private readonly IHttpRestClient _restClient;
        private readonly IMemoryCache _cache;

        public AdvertisersController(IHttpRestClient restClient, IMemoryCache cache)
        {
            _restClient = restClient;
            _cache = cache;
        }

        /// <summary>
        /// 广告主信息
        /// https://ad.toutiao.com/open_api/2/advertiser/info/
        /// </summary>
        /// <param name="advertiserIds">广告主ID集合，英文逗号分隔</param>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<IActionResult> Info([FromQuery]string advertiserIds)
        {
            var url = "https://ad.toutiao.com/open_api/2/advertiser/info/" + "?advertiser_ids=[" + advertiserIds + "]";
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 获取资质信息
        /// https://ad.toutiao.com/open_api/2/advertiser/qualification/select/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <returns></returns>
        [HttpGet("qualification")]
        public async Task<IActionResult> Qualification([FromQuery]string advertiserId)
        {
            var url = "https://ad.toutiao.com/open_api/2/advertiser/qualification/select/" + "?advertiser_id=" + advertiserId;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 广告主公开信息
        /// https://ad.toutiao.com/open_api/2/advertiser/public_info/
        /// </summary>
        /// <param name="advertiserIds">广告主ID集合，英文逗号分隔</param>
        /// <returns></returns>
        [HttpGet("public")]
        public async Task<IActionResult> Public([FromQuery]string advertiserIds)
        {
            var url = "https://ad.toutiao.com/open_api/2/advertiser/public_info/" + "?advertiser_ids=[" + advertiserIds + "]";
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }
    }
}