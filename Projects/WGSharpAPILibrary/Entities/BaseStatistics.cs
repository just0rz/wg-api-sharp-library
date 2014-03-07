using Newtonsoft.Json;

namespace WGSharpAPI.Entities
{
    public class BaseStatistics
    {
        [JsonProperty("battle_avg_xp")]
        public long BattleAvgXp { get; set; }

        [JsonProperty("battles")]
        public long Battles { get; set; }

        [JsonProperty("capture_points")]
        public long CapturePoints { get; set; }

        [JsonProperty("damage_dealt")]
        public long DamageDealt { get; set; }

        [JsonProperty("damage_received")]
        public long DamageReceived { get; set; }

        [JsonProperty("draws")]
        public long Draws { get; set; }

        [JsonProperty("dropped_capture_points")]
        public long DroppedCapturePoints { get; set; }

        [JsonProperty("frags")]
        public long Frags { get; set; }

        [JsonProperty("hits")]
        public long Hits { get; set; }

        [JsonProperty("hits_percents")]
        public long HitsPercent { get; set; }

        [JsonProperty("losses")]
        public long Losses { get; set; }

        [JsonProperty("shots")]
        public long Shots { get; set; }

        [JsonProperty("spotted")]
        public long Spotted { get; set; }

        [JsonProperty("survived_battles")]
        public long SurvivedBattles { get; set; }

        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("xp")]
        public long Xp { get; set; }
    }
}
