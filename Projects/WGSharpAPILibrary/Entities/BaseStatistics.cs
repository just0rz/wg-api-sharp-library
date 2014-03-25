/*
The MIT License (MIT)

Copyright (c) 2014 Iulian Grosu

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
