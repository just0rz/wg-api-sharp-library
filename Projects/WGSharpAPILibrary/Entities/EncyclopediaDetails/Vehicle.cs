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

namespace WGSharpAPI.Entities.EncyclopediaDetails
{
    /// <summary>
    /// Base vehicle
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Premium vehicle
        /// </summary>
        [JsonProperty("is_premium")]
        public bool IsPremium { get; set; }

        /// <summary>
        /// Tier
        /// </summary>
        [JsonProperty("level")]
        public long Tier { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Localized name field
        /// </summary>
        [JsonProperty("name_i18n")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Nation
        /// </summary>
        [JsonProperty("nation")]
        public string Nation { get; set; }

        /// <summary>
        /// Localized nation field
        /// </summary>
        [JsonProperty("nation_i18n")]
        public string LocalizedNation { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        #region Overrides

        public override string ToString()
        {
            var result = string.Empty;

            if (!string.IsNullOrWhiteSpace(LocalizedName))
                result = LocalizedName;
            else if (!string.IsNullOrWhiteSpace(Name))
                result = Name;
            else
                result = base.ToString();

            return result;
        }

        #endregion Overrides
    }
}
