using System;

namespace WGSharpAPI.Tools
{
    public static class ToolsExtensions
    {
        /// <summary>
        /// Returns the date time from unix timestamp.
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <returns></returns>
        public static DateTime DateFromWGTimestamp(this long timestamp)
        {
            var unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return unixStartDate.AddSeconds(timestamp);
        }

        /// <summary>
        /// Returns the unix timestamp from a date time.
        /// </summary>
        /// <param name="date">date time</param>
        /// <returns></returns>
        public static long DateToWGTimesptamp(this DateTime date)
        {
            var unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - unixStartDate).TotalSeconds);
        }
    }
}
