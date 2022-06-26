using Newtonsoft.Json;

namespace CaseConfig.Web.Models
{
    public class Result
    {
        [JsonProperty("success")]
        public bool Success { get; }
        [JsonProperty("message")]
        public string Message { get; }
    }
}
