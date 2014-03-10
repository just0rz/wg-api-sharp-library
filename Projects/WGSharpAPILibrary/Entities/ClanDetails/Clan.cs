using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.ClanDetails
{
    public class Clan
    {
        [JsonProperty("abbreviation")]
        public string Tag { get; set; }

        [JsonProperty("clan_id")]
        public long Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("members_count")]
        public long MembersCount { get; set; }

        [JsonProperty("motto")]
        public string Motto { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner_id")]
        public long CommanderId { get; set; }

        [JsonProperty("owner_name")]
        public string CommanderName { get; set; }

        [JsonProperty("emblems")]
        public Emblem Emblems { get; set; }
    }
}
