using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ToutiaoApi.Responses;

namespace ToutiaoApi.Controllers
{
    /// <summary>
    /// 广告创意API
    /// </summary>
    [Route("api/creatives")]
    [ApiController]
    public class CreativesController : ControllerBase
    {
        private readonly IHttpRestClient _restClient;
        private readonly IMemoryCache _cache;

        public CreativesController(IHttpRestClient restClient, IMemoryCache cache)
        {
            _restClient = restClient;
            _cache = cache;
        }

        /// <summary>
        /// 广告创意列表
        /// https://ad.toutiao.com/open_api/2/creative/select/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<IActionResult> Select([FromQuery] string advertiserId)
        {
            var url = "https://ad.toutiao.com/open_api/2/creative/select/" + "?advertiser_id=" + advertiserId + "&fields=[\"id\",\"ad_id\",\"advertiser_id\",\"title\",\"image_info\",\"image_mode\",\"status\",\"opt_status\"]";
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 创意详细信息（将废弃）
        /// https://ad.toutiao.com/open_api/2/creative/read/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <param name="advertisementId">广告ID</param>
        /// <returns></returns>
        [HttpGet("read")]
        public async Task<IActionResult> Read([FromQuery] string advertiserId, [FromQuery] string advertisementId)
        {
            var url = "https://ad.toutiao.com/open_api/2/creative/read/" + "?advertiser_id=" + advertiserId + "&ad_id=" + advertisementId;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 创意详细信息（新版）
        /// https://ad.toutiao.com/open_api/2/creative/read_v2/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <param name="advertisementId">计划ID</param>
        /// <returns></returns>
        [HttpGet("read_v2")]
        public async Task<IActionResult> ReadV2([FromQuery] string advertiserId, [FromQuery] string advertisementId)
        {
            var url = "https://ad.toutiao.com/open_api/2/creative/read_v2/" + "?advertiser_id=" + advertiserId + "&ad_id=" + advertisementId;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }
    }
}