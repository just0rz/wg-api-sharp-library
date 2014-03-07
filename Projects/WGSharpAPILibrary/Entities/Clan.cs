using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class Clan
    {
        [JsonProperty("clan_id")]
        public long Id { get; set; }

        [JsonProperty("clan.role")]
        public long Role { get; set; }

        [JsonProperty("clan.role_i18n")]
        public string LocalizedRole { get; set; }

        [JsonProperty("clan.since")]
        public long JoinedTimestamp { get; set; }
    }
}
