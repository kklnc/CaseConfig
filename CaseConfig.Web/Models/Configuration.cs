using Newtonsoft.Json;

namespace CaseConfig.Web.Models
{
    public class Configuration
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }
    }
}
