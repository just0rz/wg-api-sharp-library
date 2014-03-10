using System;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.PlayerDetails
{
    public class TankStatistics
    {
        [JsonProperty("battles")]
        public long Battles { get; set; }

        [JsonProperty("max_frags")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public long MaxFrags { get; set; }

        [JsonProperty("max_xp")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public long MaxXp { get; set; }

        [JsonProperty("win_and_survived")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public long WinAndSurvived { get; set; }

        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("clan")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public ClanStatistics Clan { get; set; }

        [JsonProperty("all")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public OverallStatistics Overall { get; set; }

        [JsonProperty("company")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public CompanyStatistics Company { get; set; }
    }
}
