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
        [JsonProperty("battles")]
        public long Battles { get; set; }

        [JsonProperty("max_frags")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long MaxFrags { get; set; }

        [JsonProperty("max_xp")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long MaxXp { get; set; }

        [JsonProperty("win_and_survived")]
        [Obsolete("Method is deprecated and has been removed.")]
        public long WinAndSurvived { get; set; }

        [JsonProperty("wins")]
        public long Wins { get; set; }

        [JsonProperty("clan")]
        [Obsolete("Method is deprecated and has been removed.")]
        public ClanStatistics Clan { get; set; }

        [JsonProperty("all")]
        [Obsolete("Method is deprecated and will be removed soon.")]
        public OverallStatistics Overall { get; set; }

        [JsonProperty("company")]
        [Obsolete("Method is deprecated and has been removed.")]
        public CompanyStatistics Company { get; set; }
    }
}
