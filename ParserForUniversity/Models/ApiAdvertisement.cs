using Newtonsoft.Json;

namespace ParserForUniversity.Models
{
    public class ApiAdvertisement
    {
        [JsonProperty("items")]
        public Model[] Models { get; set; }
    }

    public class Model
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}