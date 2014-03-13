using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WGSharpAPI.Entities.PlayerDetails;
using WGSharpAPI.Enums;
using WGSharpAPI.Interfaces;

namespace WGSharpAPI
{
    public class WGApplication : IWGApplication
    {
        private string _defaultApiURI = @"api.worldoftanks.eu/wot";
        private string _applicationId;
        private WGSettings _settings;

        #region Constructors

        public WGApplication(string applicationId)
        {
            _applicationId = applicationId;
            _settings = new WGSettings();
        }

        public WGApplication(string applicationId, WGSettings settings)
            : this(applicationId)
        {
            _settings = settings;
        }

        public WGApplication(string applicationId, WGSettings settings, string apiURI)
            : this(applicationId, settings)
        {
            _defaultApiURI = apiURI;
        }

        #endregion Constructors

        #region Search Players

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm)
        {
            return SearchPlayers(searchTerm, WGPlayerListField.All, WGLanguageField.EN, 100);
        }

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm, int limit)
        {
            return SearchPlayers(searchTerm, WGPlayerListField.All, WGLanguageField.EN, limit);
        }

        /// <summary>
        /// Method returns partial list of players. The list is filtered by initial characters of user name and sorted alphabetically.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="language">language</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Player>> SearchPlayers(string searchTerm, WGPlayerListField responseFields, WGLanguageField language, int limit)
        {
            var fields = new StringBuilder();

            if (responseFields == WGPlayerListField.AccountId)
                fields.Append("account_id");
            else if (responseFields == WGPlayerListField.Nickname)
                fields.Append("nickname");
            else
                fields.Clear();

            var requestURI = CreatePlayerSearchRequestURI(searchTerm, language, fields.ToString(), limit);

            var output = GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGResponse<List<Player>>>(output);

            return obj;
        }

        private string CreatePlayerSearchRequestURI(string searchTerm, WGLanguageField language, string responseFields, int limit)
        {
            var target = "account/list";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            sb.AppendFormat("&search={0}&limit={1}", searchTerm, limit);

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
                Count = obj.Count,
                Status = obj.Status,
                Data = players
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
        [Obsolete("Method is deprecated and will be removed soon.")]
        public IWGResponse<object> GetPlayerRatings(long accountId)
        {
            return GetPlayerRatings(new[] { accountId });
        }

        /// <summary>
        /// Method returns details on player's ratings.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        [Obsolete("Method is deprecated and will be removed soon.")]
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
        [Obsolete("Method is deprecated and will be removed soon.")]
        public IWGResponse<object> GetPlayerRatings(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            throw new NotImplementedException();
        }

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

        #region Player Tanks

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        public IWGResponse<List<Tank>> GetPlayerVehicles(long accountId)
        {
            return GetPlayerVehicles(new[] { accountId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        public IWGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds)
        {
            return GetPlayerVehicles(accountIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns details on player's vehicles.
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Tank>> GetPlayerVehicles(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreatePlayerVehiclesRequestURI(accountIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var tanks = new List<Tank>();

            foreach (var accountId in accountIds)
            {
                var player = new Player { AccountId = accountId };

                var userTanks = ((JObject)obj.Data)[accountId.ToString()].ToObject(typeof(List<Tank>)) as List<Tank>;

                foreach (var tank in userTanks)
                {
                    tank.Player = player;
                    tanks.Add(tank);
                }
            }

            var response = new WGResponse<List<Tank>>()
            {
                Count = obj.Count,
                Status = obj.Status,
                Data = tanks
            };

            return response;
        }

        private string CreatePlayerVehiclesRequestURI(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "account/tanks";

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

        #endregion Player Tanks

        #region Player Achievements

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountId">player account id</param>
        /// <returns></returns>
        public IWGResponse<object> GetPlayerAchievements(long accountId)
        {
            return GetPlayerAchievements(new[] { accountId });
        }

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <returns></returns>
        public IWGResponse<object> GetPlayerAchievements(long[] accountIds)
        {
            return GetPlayerAchievements(accountIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Warning. This method runs in test mode. Throws NotImplementedException
        /// </summary>
        /// <param name="accountIds">list of player account ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<object> GetPlayerAchievements(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreatePlayerAchievementsRequestURI(accountIds, language, accessToken, responseFields);

            throw new NotImplementedException("Warning. This method runs in test mode.");
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

        #region Authentication

        /// <summary>
        /// Method authenticates user based on Wargaming.net ID (OpenID). To log in, player must enter email and password used for creating account.
        /// The way I want to implement this might not be accepted. This will, most probably, be dropped in the future.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public string AccessToken(string username, string password)
        {
            var target = "auth/login";
            var accessToken = string.Empty;

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        /// <summary>
        /// Method generates new access_token based on the current token.
        /// This method is used when the player is still using the application but the current access_token is about to expire.
        /// </summary>
        /// <param name="accessToken">access token</param>
        /// <returns></returns>
        public string ProlongToken(string accessToken)
        {
            var target = "auth/prolongate";

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        /// <summary>
        /// Method deletes user's access_token.
        /// After this method is called, access_token becomes invalid.
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public string Logout(string accessToken)
        {
            var target = "auth/logout";

            throw new NotImplementedException("This feature has not been implemented yet");
        }

        #endregion Authentication

        #region Search Clans

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> SearchClans(string searchTerm)
        {
            return SearchClans(searchTerm, WGLanguageField.EN, null, 100, null);
        }

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> SearchClans(string searchTerm, int limit)
        {
            return SearchClans(searchTerm, WGLanguageField.EN, null, limit, null);
        }

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. 
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <param name="orderby">The list is sorted by clan name (default), creation date, tag, or size.</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> SearchClans(string searchTerm, WGLanguageField language, string responseFields, int limit, string orderby)
        {
            var requestURI = CreateClanSearchRequestURI(searchTerm, language, responseFields, limit, orderby);

            var output = this.GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGResponse<List<Entities.ClanDetails.Clan>>>(output);

            return obj;
        }

        private string CreateClanSearchRequestURI(string searchTerm, WGLanguageField language, string responseFields, int limit, string orderby)
        {
            var target = "clan/list";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            sb.AppendFormat("&search={0}&limit={1}", searchTerm, limit);

            if (!string.IsNullOrWhiteSpace(orderby))
                sb.AppendFormat("&order_by={0}", orderby);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Search Clans

        #region Clan Details

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> GetClanDetails(long clanId)
        {
            return GetClanDetails(new long[] { clanId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> GetClanDetails(long[] clanIds)
        {
            return GetClanDetails(clanIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Entities.ClanDetails.Clan>> GetClanDetails(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClanInfoRequestURI(clanIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);


            var obj = new WGResponse<List<Entities.ClanDetails.Clan>>();

            var jObject = JsonConvert.DeserializeObject<WGRawResponse>(output).Data as JObject;

            // TODO:
            // We have a problem deserializing this json due to the fact that we get an array of id fields under members
            // this can be avoided by implementing a JsonConverter and use that do parse the data correctly
            //
            // This will be done in the future because I want to see what other methods need this kind of approach
            // for the momnet it will throw a NotImplementedException

            throw new NotImplementedException();

            return obj;
        }

        private string CreateClanInfoRequestURI(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "clan/info";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&clan_id={0}", string.Join(",", clanIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Clan Details

        #region General methods

        private string GetGeneralUri(string target, WGLanguageField language)
        {
            var languageField = Enum.GetName(typeof(WGLanguageField), language).ToLowerInvariant();

            var sb = new StringBuilder();

            if (_settings.RequestProtocol == WGRequestProtocol.HTTP)
                sb.Append("http://");
            else
                sb.Append("https://");

            sb.Append(_defaultApiURI);
            if (!_defaultApiURI.EndsWith("/"))
                sb.Append('/');

            sb.AppendFormat("{0}/?application_id={1}&language={2}", target, _applicationId, languageField);

            var result = sb.ToString();

            return result;
        }

        private string GetRequestResponse(string requestURI)
        {
            IWGRequest request = new WGRequest(requestURI);

            var output = request.GetResponse();

            return output;
        }

        #endregion
    }
}