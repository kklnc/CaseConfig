using Newtonsoft.Json;

namespace CaseConfig.Web.Models
{
    public class DataResult<T> : Result
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
