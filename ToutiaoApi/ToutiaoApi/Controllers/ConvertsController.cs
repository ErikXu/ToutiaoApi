using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ToutiaoApi.Responses;

namespace ToutiaoApi.Controllers
{
    /// <summary>
    /// 转化跟踪API
    /// </summary>
    [Route("api/converts")]
    [ApiController]
    public class ConvertsController : ControllerBase
    {
        private readonly IHttpRestClient _restClient;
        private readonly IMemoryCache _cache;

        public ConvertsController(IHttpRestClient restClient, IMemoryCache cache)
        {
            _restClient = restClient;
            _cache = cache;
        }

        /// <summary>
        /// 转化ID列表
        /// https://ad.toutiao.com/open_api/2/tools/adv_convert/select/
        /// </summary>
        /// <param name="advertiserId">操作的广告主id</param>
        /// <param name="page">页数，默认值: 1</param>
        /// <param name="pageSize">页面大小，默认值: 10，最大值：100</param>
        /// <returns></returns>
        [HttpGet("select")]
        public async Task<IActionResult> Select([FromQuery] string advertiserId, [FromQuery]int page = 1, [FromQuery]int pageSize = 10)
        {
            var url = "https://ad.toutiao.com/open_api/2/tools/adv_convert/select/" + "?advertiser_id=" + advertiserId + "&page=" + page + "&page_size=" + pageSize;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }

        /// <summary>
        /// 查询转化详细信息
        /// https://ad.toutiao.com/open_api/2/tools/ad_convert/read/
        /// </summary>
        /// <param name="advertiserId">操作的广告主id</param>
        /// <param name="convertId">转化id</param>
        /// <returns></returns>
        [HttpGet("read")]
        public async Task<IActionResult> Read([FromQuery] string advertiserId, [FromQuery] string convertId)
        {
            var url = "https://ad.toutiao.com/open_api/2/tools/ad_convert/read/" + "?advertiser_id=" + advertiserId + "&convert_id=" + convertId;
            var response = await _restClient.GetAsync<DynamicResponse>(url, _cache.Get<string>(ToutiaoCacheKey.AccessToken));
            response.EnsureSuccess();
            return Ok(response);
        }
    }
}