using System.Collections.Generic;
using WGSharpAPI.Entities;
using WGSharpAPI.Enums;
using System;

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
        WGResponse<List<Player>> SearchPlayers(string searchTerm);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        WGResponse<List<Player>> SearchPlayers(string searchTerm, int limit);

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="language">language</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        WGResponse<List<Player>> SearchPlayers(string searchTerm, WGPlayerListField responseFields, WGLanguageField language, int limit);

        #endregion Search Players

        #region Player Info

        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        WGResponse<List<Player>> GetPlayerInfo(long accountId);
        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountId">list of player account ids</param>
        /// <returns></returns>
        WGResponse<List<Player>> GetPlayerInfo(long[] accountIds);
        /// <summary>
        /// Method returns player details.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        WGResponse<List<Player>> GetPlayerInfo(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);


        #endregion Player Info

        #region Player Ratings

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        WGResponse<object> GetPlayerRatings(long accountId);

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        WGResponse<object> GetPlayerRatings(long[] accountIds);

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
        WGResponse<object> GetPlayerRatings(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Ratings

        #region Player Tanks

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        WGResponse<List<Tank>> GetPlayerVehicles(long accountId);
        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        WGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds);
        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        WGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Tanks

        #region Player Achievements

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        WGResponse<object> GetPlayerAchievements(long accountId);
        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        WGResponse<object> GetPlayerAchievements(long[] accountId);
        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        WGResponse<object> GetPlayerAchievements(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);

        #endregion Player Achievements
    }
}
