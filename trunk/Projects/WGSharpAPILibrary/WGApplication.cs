using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using WGSharpAPI.Entities;
using WGSharpAPI.Enums;
using WGSharpAPI.Interfaces;
using Newtonsoft.Json.Linq;

namespace WGSharpAPI
{
    public class WGApplication : IWGApplication
    {
        private string _defaultApiURI = @"api.worldoftanks.eu/wot";
        private string _applicationId;
        private WGSettings _settings;

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

        #region Search Players

        public WGResponse<List<Player>> SearchPlayers(string searchTerm)
        {
            return SearchPlayers(searchTerm, WGPlayerListField.All, WGLanguageField.EN, 100);
        }

        public WGResponse<List<Player>> SearchPlayers(string searchTerm, int limit)
        {
            return SearchPlayers(searchTerm, WGPlayerListField.All, WGLanguageField.EN, limit);
        }

        public WGResponse<List<Player>> SearchPlayers(string searchTerm, WGPlayerListField responseFields, WGLanguageField language, int limit)
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

        public WGResponse<List<Player>> GetPlayerInfo(long accountId)
        {
            return GetPlayerInfo(new[] { accountId }, WGLanguageField.EN, null, null);
        }

        public WGResponse<List<Player>> GetPlayerInfo(long[] accountIds)
        {
            return GetPlayerInfo(accountIds, WGLanguageField.EN, null, null);
        }

        public WGResponse<List<Player>> GetPlayerInfo(long[] accountIds, WGLanguageField language, string accessToken, string responseFields)
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
            var webRequest = HttpWebRequest.Create(requestURI);
            var response = webRequest.GetResponse();

            var responseStream = response.GetResponseStream();
            string output = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream))
            {
                output = reader.ReadToEnd();
            }
            response.Close();

            return output;
        }

        #endregion
    }
}