using Newtonsoft.Json;

namespace ToutiaoApi.Responses
{
    public class Response<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        /// <summary>
        /// 参考：https://ad.toutiao.com/openapi/doc/index.html?id=225
        /// </summary>
        /// <returns></returns>
        public virtual bool IsSuccess()
        {
            return Code == 0;
        }

        public void EnsureSuccess()
        {
            if (!IsSuccess())
            {
                throw new ToutiaoException(Message);
            }
        }
    }
}