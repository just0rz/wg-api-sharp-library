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
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.ClanDetails
{
    public class Province
    {
        [JsonProperty("arena_id")]
        public long ArenaId { get; set; }

        [JsonProperty("arena_name")]
        public string ArenaName { get; set; }

        public string ArenaNameLocalized { get; set; }

        [JsonProperty("attacked")]
        public bool IsAttacked { get; set; }

        [JsonProperty("combats_running")]
        public bool IsCombatRunning { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Province owned (days)
        /// </summary>
        [JsonProperty("occupancy_time")]
        public long OccupancyTime { get; set; }

        /// <summary>
        /// Prime Time
        /// </summary>
        [JsonProperty("prime_time")]
        public long PrimeTime { get; set; }

        [JsonProperty("province_id")]
        public string Id { get; set; }

        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
