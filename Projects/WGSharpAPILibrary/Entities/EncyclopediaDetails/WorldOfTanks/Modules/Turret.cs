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

namespace WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules
{
    public class Turret : Module
    {
        /// <summary>
        /// Module ID
        /// </summary>
        [JsonProperty("module_id")]
        public long Id { get; set; }

        /// <summary>
        /// Armor: sides
        /// </summary>
        [JsonProperty("armor_board")]
        public long ArmorSides { get; set; }

        /// <summary>
        /// Armor: rear
        /// </summary>
        [JsonProperty("armor_fedd")]
        public long ArmorRear { get; set; }

        /// <summary>
        /// Armor: front
        /// </summary>
        [JsonProperty("armor_forehead")]
        public long ArmorFront { get; set; }

        /// <summary>
        /// View Range
        /// </summary>
        [JsonProperty("circular_vision_radius")]
        public long ViewRange { get; set; }

        /// <summary>
        /// Traverse speed
        /// </summary>
        [JsonProperty("rotation_speed")]
        public long TraverseSpeed { get; set; }

        /// <summary>
        /// Cost of research in experience
        /// </summary>
        [JsonProperty("price_xp")]
        [Obsolete("Warning. The field will be disabled.")]
        public long? PriceXp { get; set; }

        /// <summary>
        /// Cost of research in experience
        /// </summary>
        [JsonProperty("weight")]
        [Obsolete("Warning. The field will be disabled.")]
        public decimal? Weight { get; set; }
    }
}
