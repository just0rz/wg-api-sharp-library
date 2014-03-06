using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class PlayerStatistics
    {
        [JsonProperty("clan")]
        public PlayerClanStatistics Clan { get; set; }

        [JsonProperty("all")]
        public PlayerRandomStatistics Random { get; set; }

        [JsonProperty("company")]
        public PlayerCompaniesStatistics Companies { get; set; }

        [JsonProperty("max_xp")]
        public long MaxXp { get; set; }
    }
}
