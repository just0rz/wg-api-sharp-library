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
using System;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.PlayerDetails
{
    public class TankStatistics
    {

        [JsonProperty("clan")]
        public ClanStatistics Clan { get; set; }

        [JsonProperty("all")]
        public OverallStatistics Overall { get; set; }

        /// <summary>
        /// Player Owner
        /// </summary>
        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        [JsonProperty("max_xp")]
        public long MaxXp { get; set; }

        [JsonProperty("company")]
        public CompanyStatistics Company { get; set; }

        [JsonProperty("battles")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long Battles { get; set; }

        [JsonProperty("max_frags")]
        public long MaxFrags { get; set; }

        [JsonProperty("mark_of_mastery")]
        public long MarkOfMastery { get; set; }

        [JsonProperty("in_garage")]
        public bool InGarage { get; set; }

        [JsonProperty("tank_id")]
        public bool TankId { get; set; }

        [JsonProperty("win_and_survived")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long WinAndSurvived { get; set; }

        [JsonProperty("wins")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long Wins { get; set; }
    }
}
