using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ToutiaoApi.Responses;

namespace ToutiaoApi.Controllers
{
    /// <summary>
    /// 广告计划API
    /// </summary>
    [Route("api/advertisements")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IHttpRestClient _restClient;
        private readonly IMemoryCache _cache;

        public AdvertisementsController(IHttpRestClient restClient, IMemoryCache cache)
        {
            _restClient = restClient;
            _cache = cache;
        }

        /// <summary>
        /// 获取广告计划（新版）
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <param name="page">页数，默认值: 1</param>
        /// <param name="pageSize">页面大小，默认值: 10，最大值：100</param>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] string advertiserId, [FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var url = "https://ad.toutiao.com/open_api/2/ad/get/" + "?advertiser_id=" + advertiserId + "&page=" + page + "&page_size=" + pageSize;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 广告计划列表（将废弃）
        /// https://ad.toutiao.com/open_api/2/ad/select/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<IActionResult> Select([FromQuery] string advertiserId)
        {
            var url = "https://ad.toutiao.com/open_api/2/ad/select/" + "?advertiser_id=" + advertiserId + "&fields=[\"id\",\"name\",\"campaign_id\",\"advertiser_id\",\"status\",\"opt_status\"]";
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 计划详细信息（将废弃）
        /// https://ad.toutiao.com/open_api/2/ad/read/
        /// </summary>
        /// <param name="advertiserId">广告主ID</param>
        /// <param name="advertisementIds">计划ID集合，英文逗号分隔</param>
        /// <returns></returns>
        [HttpGet("read")]
        public async Task<IActionResult> Read([FromQuery] string advertiserId, [FromQuery] string advertisementIds)
        {
            var url = "https://ad.toutiao.com/open_api/2/ad/read/" + "?advertiser_id=" + advertiserId + "&ad_ids=[" + advertisementIds + "]";
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }
    }
}