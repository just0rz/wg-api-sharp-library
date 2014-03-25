﻿/*
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