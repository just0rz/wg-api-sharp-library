/*
The MIT License (MIT)

Copyright (c) 2016 Iulian Grosu

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System.Collections.Generic;
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
