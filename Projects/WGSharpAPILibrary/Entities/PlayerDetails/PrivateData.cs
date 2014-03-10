using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.PlayerDetails
{
    public class PrivateData
    {
        [JsonProperty("account_type")]
        public string AccountType { get; set; }

        [JsonProperty("account_type_i18n")]
        public string LocalizedAccountType { get; set; }

        [JsonProperty("ban_info")]
        public string AccountBanDetails { get; set; }

        [JsonProperty("ban_time")]
        public long EndTimeOfAccountBan { get; set; }

        [JsonProperty("credits")]
        public long Credits { get; set; }

        [JsonProperty("free_xp")]
        public long FreeExperience { get; set; }

        [JsonProperty("friends")]
        public List<long> FriendIds { get; set; }

        [JsonProperty("gold")]
        public long Gold { get; set; }

        [JsonProperty("is_bound_to_phone")]
        public long HasMobile { get; set; }

        [JsonProperty("is_premium")]
        public long HasPremium { get; set; }

        [JsonProperty("premium_expires_at")]
        public long PremiumAccountExpiresAt { get; set; }

        [JsonProperty("restrictions.chat_ban_time")]
        public long ChatRestrictionsExpiresAt { get; set; }

        [JsonProperty("restrictions.clan_time")]
        public long ClanRestrictionsExpiresAt { get; set; }
    }
}
