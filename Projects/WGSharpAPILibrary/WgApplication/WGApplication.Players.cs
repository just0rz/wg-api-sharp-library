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
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WGSharpAPI.Entities.ClanDetails;
using WGSharpAPI.Entities.PlayerDetails;
using WGSharpAPI.Enums;
using WGSharpAPI.Interfaces;
using Clan = WGSharpAPI.Entities.ClanDetails.Clan;
using WotEncyclopedia = WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks;
using System.Diagnostics.CodeAnalysis;
using WGSharpAPI.Tools;

namespace WGSharpAPI
{
    public partial class WGApplication : IWGApplication
    {
        #region Account

        #region Search Players

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm)
        {
            return SearchPlayers(searchTerm, null, WGLanguageField.EN, WGSearchType.StartsWith, 100);
        }

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGSearchType searchType)
        {
            return SearchPlayers(searchTerm, null, WGLanguageField.EN, searchType, 100);
        }

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGSearchType searchType, int limit)
        {
            return SearchPlayers(searchTerm, null, WGLanguageField.EN, searchType, limit);
        }

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="language">language</param>
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm, string responseFields, WGLanguageField language, WGSearchType searchType, int limit)
        {
            var requestURI = CreatePlayerSearchRequestURI(searchTerm, language, responseFields, searchType, limit);

            var output = GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGResponse<List<Player>>>(output);

            return obj;
        }

        private string CreatePlayerSearchRequestURI(string searchTerm, WGLanguageField language, string responseFields, WGSearchType searchType, int limit)
        {
            var target = "account/list";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            var searchTypeField = EnumHelper<WGSearchType>.GetEnumDescription(searchType);

            sb.AppendFormat("&type={0}&search={1}&limit={2}", searchTypeField, searchTerm, limit);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Search Players

        #region Player Info

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerInfo(long accountId)
        {
            return GetPlayerInfo(new[] { accountId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">list of player account ids</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerInfo(long[] accountIds)
        {
            return GetPlayerInfo(accountIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerInfo(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreatePlayerDataRequestURI(accountIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var players = new List<Player>();

            foreach (var playerWrapper in ((JObject)obj.Data).Children())
            {
                players.Add(playerWrapper.First.ToObject(typeof(Player)) as Player);
            }

            var response = new WGResponse<List<Player>>()
            {
                Status = obj.Status,
                Meta = obj.Meta,
                Data = players,
            };

            return response;
        }

        private string CreatePlayerDataRequestURI(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "account/info";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", string.Join(",", accountIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Player Info

        #region Player Ratings

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        [Obsolete("Method has been removed.")]
        [ExcludeFromCodeCoverage]
        public IWGResponse<object> GetPlayerRatings(long accountId)
        {
            return GetPlayerRatings(new[] { accountId });
        }

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        [Obsolete("Method has been removed.")]
        [ExcludeFromCodeCoverage]
        public IWGResponse<object> GetPlayerRatings(long[] accountIds)
        {
            return GetPlayerRatings(accountIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        [Obsolete("Method has been removed.")]
        public IWGResponse<object> GetPlayerRatings(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            throw new NotImplementedException();
        }

        [ExcludeFromCodeCoverage]
        private string CreatePlayerRatingsRequestURI(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "account/ratings";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", string.Join(",", accountIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Player Ratings

        #region Player Vehicles

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerVehicles(long accountId)
        {
            return GetPlayerVehicles(new[] { accountId }, new long[0], WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerVehicles(long[] accountIds)
        {
            return GetPlayerVehicles(accountIds, new long[0], WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="tankIds">list of tank ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerVehicles(long[] accountIds, long[] tankIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreatePlayerVehiclesRequestURI(accountIds, tankIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.PlayerDetails.Player>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.PlayerDetails.Player>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var accountId in accountIds)
            {
                var accountIdString = accountId.ToString();
                var userTanks = jObject[accountIdString].ToObject(typeof(List<WGSharpAPI.Entities.PlayerDetails.Tank>)) as List<WGSharpAPI.Entities.PlayerDetails.Tank>;

                var player = new Player { AccountId = accountId, Tanks = userTanks };

                foreach (var tank in player.Tanks)
                {
                    tank.Player = player;
                }

                obj.Data.Add(player);
            }

            return obj;
        }

        private string CreatePlayerVehiclesRequestURI(long[] accountIds, long[] tankIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "account/tanks";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", string.Join(",", accountIds));

            if (tankIds.Length > 0)
                sb.AppendFormat("&tank_id={0}", string.Join(",", tankIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Player Vehicles

        #region Player Achievements

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        public IWGResponse<List<Achievement>> GetPlayerAchievements(long accountId)
        {
            var result = GetPlayerAchievements(new[] { accountId });

            var partialResult = new WGResponse<List<Achievement>>
            {
                Status = result.Status,
                Data = new List<Achievement>(),
            };

            // if we get a bad/empty answer
            if (result.Status != "ok" || result.Data.Count == 0)
                return partialResult;

            // otherwise populate our object
            partialResult.Data = result.Data[0].Achievements;

            return partialResult;
        }

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerAchievements(long[] accountIds)
        {
            return GetPlayerAchievements(accountIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> GetPlayerAchievements(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreatePlayerAchievementsRequestURI(accountIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<Player>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Player>()
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var accountId in accountIds)
            {
                var accountIdString = accountId.ToString();
                var player = new Player { AccountId = accountId, Achievements = new List<Achievement>() };

                var jObjectAchievements = jObject[accountIdString]["achievements"];

                foreach (var jObjAchievement in jObjectAchievements)
                {
                    var jProp = (JProperty)jObjAchievement;
                    var achievement = new Achievement { Name = jProp.Name, TimesAchieved = jObjAchievement.ToObject<long>() };
                    player.Achievements.Add(achievement);
                }

                obj.Data.Add(player);
            }

            return obj;
        }

        private string CreatePlayerAchievementsRequestURI(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "account/achievements";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", string.Join(",", accountIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Player Achievements

        #endregion Account

        #region Authentication

        /// <summary>
        /// Method authenticates user based on Wargaming.net ID (OpenID). To log in, player must enter email and password used for creating account.
        /// The way I want to implement this might not be accepted. This will, most probably, be dropped in the future.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public string AccessToken(string username, string password)
        {
            //var target = "auth/login";
            var accessToken = string.Empty;

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        /// <summary>
        /// Method generates new access_token based on the current token.
        /// This method is used when the player is still using the application but the current access_token is about to expire.
        /// </summary>
        /// <param name="accessToken">access token</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public string ProlongToken(string accessToken)
        {
            //var target = "auth/prolongate";

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        /// <summary>
        /// Method deletes user's access_token.
        /// After this method is called, access_token becomes invalid.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public string Logout(string accessToken)
        {
            //var target = "auth/logout";

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        #endregion Authentication
    }
}
