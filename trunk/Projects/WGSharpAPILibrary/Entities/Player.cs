using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class Player
    {
        #region JSON Properties

        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("clan")]
        [Obsolete("This field will be removed")]
        public Clan Clan { get; set; }

        [JsonProperty("achievements")]
        public Achievements Achievements { get; set; }

        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        [JsonProperty("private")]
        public PrivateData Private { get; set; }

        #endregion JSON Properties

        /// <summary>
        /// List of tanks
        /// </summary>
        public List<Tank> Tanks { get; set; }

        #region Overrides

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Nickname) ? base.ToString() : Nickname;
        }

        #endregion Overrides
    }
}
