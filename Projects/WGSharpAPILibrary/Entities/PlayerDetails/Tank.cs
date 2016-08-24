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
    /// <summary>
    /// Player Tank
    /// </summary>
    public class Tank : WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank
    {
        public Tank()
        {
            Achievements = new List<Achievement>();
        }

        private long masteryBadge = -1;
        /// <summary>
        /// Mastery Badges
        /// -1 - Value not retrieved, 0 — None,  1 — 3rd Class, 2 — 2nd Class, 3 — 1st Class, 4 — Ace Tanker
        /// </summary>
        [JsonProperty("mark_of_mastery")]
        public long MasteryBadge { get { return masteryBadge; } set { masteryBadge = value; } }

        /// <summary>
        /// Vehicle statistics
        /// </summary>
        [JsonProperty("statistics")]
        public TankStatistics TankStatistics { get; set; }

        /// <summary>
        /// Overall Statistics
        /// </summary>
        [JsonProperty("all")]
        public OverallStatistics Overall { get; set; }

        /// <summary>
        /// Clan battles statistics
        /// </summary>
        [JsonProperty("clan")]
        public ClanStatistics Clan { get; set; }

        /// <summary>
        /// Tank Company battles statistics
        /// </summary>
        [JsonProperty("company")]
        public CompanyStatistics Company { get; set; }

        /// <summary>
        /// Team battles statistics
        /// </summary>
        [JsonProperty("team")]
        public TeamStatistics Team { get; set; }

        /// <summary>
        /// Maximum destroyed in battle
        /// </summary>
        [JsonProperty("max_frags")]
        public int MaxFrags { get; set; }

        /// <summary>
        /// Maximum experience per battle
        /// </summary>
        [JsonProperty("max_xp")]
        public int MaxXp { get; set; }

        // ToDo: Add support for "frags" and "in_garage"

        /// <summary>
        /// Tank owner
        /// </summary>
        [JsonIgnore]
        public Player Player { get; set; }

        /// <summary>
        /// Tank achievements
        /// </summary>
        [JsonIgnore]
        public List<Achievement> Achievements { get; set; }

        #region Overrides

        public override string ToString()
        {
            return Id != 0 ? Id.ToString() : base.ToString();
        }

        #endregion Overrides
    }
}
