using Newtonsoft.Json;
using System;

namespace WGSharpAPI.Entities.PlayerDetails
{
    [JsonObject("clan")]
    [Obsolete("This field will be removed")]
    public class Clan
    {
        [JsonProperty("clan_id")]
        public long Id { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("role_i18n")]
        public string LocalizedRole { get; set; }

        [JsonProperty("since")]
        public long JoinedTimestamp { get; set; }
    }
}
