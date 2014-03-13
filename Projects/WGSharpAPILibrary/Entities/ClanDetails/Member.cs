using System;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.ClanDetails
{
    [JsonArray("members")]
    public class Member
    {
        /// <summary>
        /// Clan member account id. Equivalent to a player's account id.
        /// </summary>
        [JsonProperty("account_id")]
        public long Id { get; set; }

        [JsonProperty("account_name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public long DateJoined { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("role_i18n")]
        public string LocalizedRole { get; set; }

        [Obsolete("Warning. The field will be disabled.")]
        [JsonProperty("updated_at")]
        public long DateDetailsUpdated { get; set; }
    }
}
