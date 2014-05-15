using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements
{
    public class TankAchievement : Achievement
    {
        /// <summary>
        /// Section order
        /// </summary>
        [JsonProperty("section_order")]
        public long SectionOrder { get; set; }

        /// <summary>
        /// Service Record
        /// </summary>
        [JsonProperty("options")]
        public List<AchievementOption> Options { get; set; }
    }
}
