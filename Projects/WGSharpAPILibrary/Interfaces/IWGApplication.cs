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
using WGSharpAPI.Entities.ClanDetails;
using WGSharpAPI.Entities.PlayerDetails;
using WGSharpAPI.Enums;
using Clan = WGSharpAPI.Entities.ClanDetails.Clan;
using WotEncyclopedia = WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks;

namespace WGSharpAPI.Interfaces
{
    /// <summary>
    /// Represents your application identified by an application id.
    /// You can get one from WG's developer room: http://wargaming.net/developers/
    /// </summary>
    public interface IWGApplication
    {
        #region Account

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
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGSearchType searchType);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGSearchType searchType, int limit);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="language">language</param>
        /// <param name="searchTerm">search type: 'startswith' or 'exact'</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        IWGResponse<List<Player>> SearchPlayers(string searchTerm, string responseFields, WGLanguageField language, WGSearchType searchType, int limit);

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
        IWGResponse<List<WGSharpAPI.Entities.PlayerDetails.Player>> GetPlayerVehicles(long accountId);

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        IWGResponse<List<WGSharpAPI.Entities.PlayerDetails.Player>> GetPlayerVehicles(long[] accountIds);

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<WGSharpAPI.Entities.PlayerDetails.Player>> GetPlayerVehicles(long[] accountIds, long[] tankIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Tanks

        #region Player Achievements

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        IWGResponse<List<Achievement>> GetPlayerAchievements(long accountId);

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        IWGResponse<List<Player>> GetPlayerAchievements(long[] accountId);

        /// <summary>
        /// Returns a list of player achievements.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Player>> GetPlayerAchievements(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

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

        #region Clans

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
        IWGResponse<List<Battle>> GetClansBattles(long clanId);

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        IWGResponse<List<Battle>> GetClansBattles(long[] clanIds);

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Battle>> GetClansBattles(long[] clanIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Clan's Battles

        #region Top Clans by Victory Points

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetTopClansByVictoryPoints();

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetTopClansByVictoryPoints(WGTimeDelta time);

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        IWGResponse<List<Clan>> GetTopClansByVictoryPoints(WGTimeDelta time, WGLanguageField language, string responseFields);

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

        #endregion Clans

        #region Encyclopedia

        #region List Vehicles

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Tank>> GetAllVehicles();

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Tank>> GetAllVehicles(WGLanguageField language, string responseFields);

        #endregion List Vehicles

        #region Vehicle Details

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tankId"></param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Tank>> GetVehicleDetails(long tankId);

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Tank>> GetVehicleDetails(long[] tankIds);

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Tank>> GetVehicleDetails(long[] tankIds, WGLanguageField language, string responseFields);

        #endregion Vehicle Details

        #region Engines

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Engine>> GetEngines();

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Engine>> GetEngines(long moduleId);

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Engine>> GetEngines(long[] moduleIds);

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Engine>> GetEngines(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields);

        #endregion Engines

        #region Turrets

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Turret>> GetTurrets();

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Turret>> GetTurrets(long moduleId);

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Turret>> GetTurrets(long[] moduleIds);

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Turret>> GetTurrets(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields);

        #endregion Turrets

        #region Radios

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Radio>> GetRadios();

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Radio>> GetRadios(long moduleId);

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Radio>> GetRadios(long[] moduleIds);

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Radio>> GetRadios(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields);

        #endregion Radios

        #region Suspensions

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Chassis>> GetSuspensions();

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Chassis>> GetSuspensions(long moduleId);

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Chassis>> GetSuspensions(long[] moduleIds);

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Chassis>> GetSuspensions(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields);

        #endregion Suspensions

        #region Guns

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Gun>> GetGuns();

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Gun>> GetGuns(long moduleId);

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Gun>> GetGuns(long[] moduleIds);

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Modules.Gun>> GetGuns(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields);

        #endregion Guns

        #region Achievements

        /// <summary>
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Achievements.TankAchievement>> GetAchievements();

        /// <summary>
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        IWGResponse<List<WotEncyclopedia.Achievements.TankAchievement>> GetAchievements(WGLanguageField language, string responseFields);

        #endregion Achievements

        #endregion Encyclopedia

        #region Player's vehicles

        #region Vehicle statistics

        /// <summary>
        /// Method returns overall statistics, Tank Company statistics, and clan statistics per each vehicle for a user.
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <returns></returns>
        IWGResponse<List<Tank>> GetTankStats(long accountId);

        /// <summary>
        /// Method returns overall statistics, Tank Company statistics, and clan statistics per each vehicle for each user.
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="tankIds">list of player vehicle ids</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned</param>
        /// <param name="accessToken">access token</param>
        /// <param name="inGarage">Filter by vehicle availability in the Garage. If the parameter is not specified, all vehicles are returned. Valid values: "1" — Return vehicles available in the Garage. "0" — Return vehicles that are no longer in the Garage.</param>
        /// <returns></returns>
        IWGResponse<List<Tank>> GetTankStats(long accountId, long[] tankIds, WGLanguageField language, string responseFields, string accessToken, bool? inGarage);

        #endregion Vehicle statistics

        #region Vehicle achievements

        /// <summary>
        /// Method returns list of vehicles and achievements per vehicle for a user.
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <returns></returns>
        IWGResponse<Player> GetTankAchievements(long accountId);

        /// <summary>
        /// Method returns list of vehicles and achievements per vehicle a user.
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="tankIds">list of player vehicle ids</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned</param>
        /// <param name="accessToken">access token</param>
        /// <param name="inGarage">Filter by vehicle availability in the Garage. If the parameter is not specified, all vehicles are returned. Valid values: "1" — Return vehicles available in the Garage. "0" — Return vehicles that are no longer in the Garage.</param>
        /// <returns></returns>
        IWGResponse<Player> GetTankAchievements(long accountId, long[] tankIds, WGLanguageField language, string responseFields, string accessToken, bool? inGarage);

        #endregion Vehicle achievements

        #endregion Player's vehicles
    }
}
