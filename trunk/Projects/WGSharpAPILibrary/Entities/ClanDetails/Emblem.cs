using Newtonsoft.Json;

namespace WGSharpAPI.Entities.ClanDetails
{
    public class Emblem
    {
        [JsonProperty("bw_tank")]
        public string Tank { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("small")]
        public string Small { get; set; }
    }
}
