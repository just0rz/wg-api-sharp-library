using System;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class Tank
    {
        /// <summary>
        /// Tank owner
        /// </summary>
        public Player Player { get; set; }

        [JsonProperty("achievements")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public Achievements Achievements { get; set; }

        [JsonProperty("statistics")]
        public TankStatistics Statistics { get; set; }

        [JsonProperty("mark_of_mastery")]
        public long MasteryBadge { get; set; }

        [JsonProperty("last_battle_time")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public long LastTimeInBattle { get; set; }

        [JsonProperty("in_garage")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public long InGarage { get; set; }

        [JsonProperty("tank_id")]
        public long Id { get; set; }
    }
}
