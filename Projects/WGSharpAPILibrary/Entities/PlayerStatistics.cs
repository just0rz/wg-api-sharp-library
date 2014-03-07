using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class Statistics
    {
        [JsonProperty("clan")]
        public ClanStatistics Clan { get; set; }

        [JsonProperty("all")]
        public OverallStatistics Overall { get; set; }

        [JsonProperty("company")]
        public CompanyStatistics Company { get; set; }

        [JsonProperty("max_xp")]
        public long MaxXp { get; set; }
    }
}
