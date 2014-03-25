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
using System.Collections.Generic;
using WGSharpAPI.Enums;
using WGSharpAPI.Entities.ClanDetails;
using WGSharpAPI.Entities.PlayerDetails;
using Clan = WGSharpAPI.Entities.ClanDetails.Clan;

namespace WGSharpAPI.Interfaces
{
    public interface IWGApplication
    {
        #region Search Players

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm, int limit);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="language">language</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGPlayerListField responseFields, WGLanguageField language, int limit);

        #endregion Search Players

        #region Player Info

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        IWGResponse<List<Player>> GetPlayerInfo(long accountId);

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">list of player account ids</param>
        /// <returns></returns>
        IWGResponse<List<Player>> GetPlayerInfo(long[] accountIds);

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Player>> GetPlayerInfo(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Info

        #region Player Ratings

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        IWGResponse<object> GetPlayerRatings(long accountId);

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        IWGResponse<object> GetPlayerRatings(long[] accountIds);

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        IWGResponse<object> GetPlayerRatings(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Ratings

        #region Player Tanks

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        IWGResponse<List<Tank>> GetPlayerVehicles(long accountId);

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        IWGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds);

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Tanks

        #region Player Achievements

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        IWGResponse<object> GetPlayerAchievements(long accountId);

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        IWGResponse<object> GetPlayerAchievements(long[] accountId);

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<object> GetPlayerAchievements(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Achievements

        #region Authentication

        /// <summary>
        /// Method authenticates user based on Wargaming.net ID (OpenID). To log in, player must enter email and password used for creating account.
        /// The way I want to implement this might not be accepted. This will, most probably, be dropped in the future.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        string AccessToken(string username, string password);

        /// <summary>
        /// Method generates new access_token based on the current token.
        /// This method is used when the player is still using the application but the current access_token is about to expire.
        /// </summary>
        /// <param name="accessToken">access token</param>
        /// <returns></returns>
        string ProlongToken(string accessToken);

        /// <summary>
        /// Method deletes user's access_token.
        /// After this method is called, access_token becomes invalid.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        string Logout(string accessToken);

        #endregion Authentication

        #region Search Clans

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> SearchClans(string searchTerm);

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> SearchClans(string searchTerm, int limit);

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. 
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <param name="orderby">The list is sorted by clan name (default), creation date, tag, or size.</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> SearchClans(string searchTerm, WGLanguageField language, string responseFields, int limit, string orderby);

        #endregion Search Clans

        #region Clan Details

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetClanDetails(long clanId);

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetClanDetails(long[] clanIds);

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetClanDetails(long[] clanIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan Details

        #region Clan's Battles

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        WGRawResponse GetClansBattles(long clanId);

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        WGRawResponse GetClansBattles(long[] clanIds);

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        WGRawResponse GetClansBattles(long[] clanIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan's Battles

        #region Top Clans by Victory Points

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <returns></returns>
        WGRawResponse GetTopClansByVictoryPoints();

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <returns></returns>
        WGRawResponse GetTopClansByVictoryPoints(string time);

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        WGRawResponse GetTopClansByVictoryPoints(string time, WGLanguageField language, string responseFields);

        #endregion Top Clans by Victory Points

        #region Clan's Provinces

        /// <summary>
        /// Method returns list of clan's provinces.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        IWGResponse<List<Province>> GetClansProvinces(long clanId);

        /// <summary>
        /// Method returns list of clan's provinces.
        /// </summary>
        /// <param name="clanIds">clan id</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Province>> GetClansProvinces(long clanId, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan's Provinces

        #region Clan's Victory Points

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        IWGResponse<List<long>> GetClansVictoryPoints(long clanId);

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        IWGResponse<List<long>> GetClansVictoryPoints(long[] clanIds);

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<long>> GetClansVictoryPoints(long[] clanIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan's Victory Points

        #region Clan Member Details

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanId">member id</param>
        /// <returns></returns>
        IWGResponse<List<Member>> GetClanMemberInfo(long memberId);

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanIds">list of clan member ids</param>
        /// <returns></returns>
        IWGResponse<List<Member>> GetClanMemberInfo(long[] memberIds);

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanIds">list of clan member ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Member>> GetClanMemberInfo(long[] memberIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan Member Details
    }
}
