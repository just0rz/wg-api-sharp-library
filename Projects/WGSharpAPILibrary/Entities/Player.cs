using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    [JsonObjectAttribute]
    public class Player
    {
        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("clan")]
        public PlayerClan Clan { get; set; }

        [JsonProperty("achievements")]
        public PlayerAchievements Achievements { get; set; }

        [JsonProperty("statistics")]
        public PlayerStatistics Statistics { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("private")]
        public string Private { get; set; }
    }
}
