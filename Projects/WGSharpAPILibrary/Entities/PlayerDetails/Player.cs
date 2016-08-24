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
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.PlayerDetails
{
    public class Player
    {
        public Player()
        {
            Achievements = new List<Achievement>();
            Tanks = new List<Tank>();
        }

        /// <summary>
        /// Player account ID
        /// </summary>
        [JsonProperty("account_id")]
        public long AccountId { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// Date when player's account was created
        /// </summary>
        [JsonProperty("created_at")]
        public long CreatedAt { get; set; }

        /// <summary>
        /// Personal rating
        /// </summary>
        [JsonProperty("global_rating")]
        public decimal GlobalRating { get; set; }

        /// <summary>
        /// Last battle time
        /// </summary>
        [JsonProperty("last_battle_time")]
        public long LastBattleTime { get; set; }

        /// <summary>
        /// End time of last game session
        /// </summary>
        [JsonProperty("logout_at")]
        public long LastLogout { get; set; }

        /// <summary>
        /// Date when player details were updated
        /// </summary>
        [JsonProperty("updated_at")]
        public long UpdatedAt { get; set; }

        /// <summary>
        /// Player's achievements
        /// </summary>
        [JsonProperty("achievements")]
        [Obsolete("This field will be removed.")]
        public Achievements AchievementsOld { get; set; }

        /// <summary>
        /// Player's private data
        /// </summary>
        [JsonProperty("private")]
        public PrivateData Private { get; set; }

        /// <summary>
        /// Player statistics
        /// </summary>
        [JsonProperty("statistics")]
        public Statistics Statistics { get; set; }

        /// <summary>
        /// Player's achievements
        /// </summary>
        public List<Achievement> Achievements { get; set; }

        /// <summary>
        /// List of tanks
        /// </summary>
        public List<Tank> Tanks { get; set; }

        #region Overrides

        public override string ToString()
        {
            var result = string.Empty;

            if (!string.IsNullOrWhiteSpace(Nickname))
                result = Nickname;
            else if (AccountId != 0)
                result = AccountId.ToString();
            else
                result = base.ToString();

            return result;
        }

        #endregion Overrides
    }
}
