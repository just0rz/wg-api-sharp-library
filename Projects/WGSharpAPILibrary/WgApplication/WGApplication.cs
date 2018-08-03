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
    /// <summary>
    /// Represents your application identified by an application id.
    /// You can get one from WG's developer room: http://developers.wargaming.net/
    /// </summary>
    public partial class WGApplication : IWGApplication
    {
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

        private IWGRequest _wgRequest;
        public IWGRequest Request
        {
            get
            {
                if (_wgRequest.IsConsumed)
                {
                    _wgRequest = WGRequest.GetRequest();
                }
                return _wgRequest;
            }
            set
            {
                _wgRequest = value;
            }
        }

        #endregion Constructors

        #region Clans

        #region Search Clans

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <returns></returns>
        public IWGResponse<List<Clan>> SearchClans(string searchTerm)
        {
            return SearchClans(searchTerm, WGLanguageField.EN, null, 100, null);
        }

        /// <summary>
        /// Method returns partial list of clans filtered by initial characters of clan name or tag. The list is sorted by clan nameby default.
        /// </summary>
        /// <param name="searchTerm">search string</param>
        /// <param name="limit">Maximum number of results to be returned. limit max value is 100</param>
        /// <returns></returns>
        public IWGResponse<List<Clan>> SearchClans(string searchTerm, int limit)
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
        public IWGResponse<List<Clan>> SearchClans(string searchTerm, WGLanguageField language, string responseFields, int limit, string orderby)
        {
            var requestURI = CreateClanSearchRequestURI(searchTerm, language, responseFields, limit, orderby);

            var output = this.GetRequestResponse(requestURI);

            var obj = JsonConvert.DeserializeObject<WGResponse<List<Clan>>>(output);

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
        public IWGResponse<List<Clan>> GetClanDetails(long clanId)
        {
            return GetClanDetails(new long[] { clanId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns clan details.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        public IWGResponse<List<Clan>> GetClanDetails(long[] clanIds)
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
        public IWGResponse<List<Clan>> GetClanDetails(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClanInfoRequestURI(clanIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            // this is our raw response which we will parse later on
            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            // JObject accepts Language-INtegrated Queries over it, so it's our friend
            var jObject = wgRawResponse.Data as JObject;

            // copy the response details from the raw response to an actual response
            var obj = new WGResponse<List<Clan>>()
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Clan>(),
            };

            // were there any problems?
            if (obj.Status != "ok")
                return obj;

            // everything went fine
            // let's begin with some nasty parsing :(
            foreach (var clanId in clanIds)
            {
                // I don't really like calling methods in the indexer - this should improve readability
                var stringClanId = clanId.ToString();

                // create our empty clan entity
                var clan = new Clan();

                // get the json string for the clan id
                var clanJsonString = jObject[stringClanId];

                // parse the json string and retrieve all the fields that we can
                clan = clanJsonString.ToObject<Clan>();

                #region parse members in clan

                // get the list of members array
                var listOfMembers = clanJsonString["members"];

                // any members? -> the answer is expected to be true, but we ask anyway
                if (listOfMembers.HasValues)
                {
                    // get the children json strings for each member
                    var listOfActualMembers = listOfMembers.Children();

                    // go through our list
                    foreach (var member in listOfActualMembers)
                    {
                        // get the json string
                        var memberJsonString = member.First;

                        // parse the json string and get a Member entity
                        var parsedMember = memberJsonString.ToObject<Member>();

                        // add each parsed member to our clan list
                        clan.Members.Add(parsedMember);
                    }
                }

                #endregion parse members in clan

                // add the clan to the our actual response data
                obj.Data.Add(clan);
            }

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

        #region Clan's Battles

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        public IWGResponse<List<Battle>> GetClansBattles(long clanId)
        {
            return GetClansBattles(new long[] { clanId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        public IWGResponse<List<Battle>> GetClansBattles(long[] clanIds)
        {
            return GetClansBattles(clanIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns list of clan's battles.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Battle>> GetClansBattles(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClanBattlesRequestURI(clanIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<Battle>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Battle>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            // nastiest parsing so far
            foreach (var clanId in clanIds)
            {
                var clanBattle = new Battle { Clan = new Clan { Id = clanId } };
                var clanIdString = clanId.ToString();
                var clanBattleJObject = jObject[clanIdString].First;

                foreach (var provinceJObject in clanBattleJObject["provinces"])
                {
                    var province = new Province { Name = provinceJObject.ToString() };

                    clanBattle.Provinces.Add(province);
                }

                clanBattle.Started = clanBattleJObject["started"].ToObject<bool>();

                clanBattle.StartTime = clanBattleJObject["time"].ToObject<long>();

                foreach (var arenaJObject in clanBattleJObject["arenas"])
                {
                    var arena = new Province { ArenaNameLocalized = arenaJObject["name_i18n"].ToString(), ArenaName = arenaJObject["name"].ToString() };

                    clanBattle.Arenas.Add(arena);
                }

                var battleType = clanBattleJObject["type"].ToString();

                if (battleType == "for_province")
                    clanBattle.Type = WGBattleType.Province;
                else if (battleType == "meeting_engagement")
                    clanBattle.Type = WGBattleType.Encounter;
                else
                    clanBattle.Type = WGBattleType.Landing;

                obj.Data.Add(clanBattle);
            }

            return obj;
        }

        private string CreateClanBattlesRequestURI(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "clan/battles";

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

        #endregion Clan's Battles

        #region Top Clans by Victory Points

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<Clan>> GetTopClansByVictoryPoints()
        {
            return GetTopClansByVictoryPoints(WGTimeDelta.Season);
        }

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <returns></returns>
        public IWGResponse<List<Clan>> GetTopClansByVictoryPoints(WGTimeDelta time)
        {
            return GetTopClansByVictoryPoints(time, WGLanguageField.EN, null);
        }

        /// <summary>
        /// Method returns top 100 clans sorted by rating.
        /// </summary>
        /// <param name="time">Time delta. Valid values: current_season (default), current_step</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Clan>> GetTopClansByVictoryPoints(WGTimeDelta time, WGLanguageField language, string responseFields)
        {
            var requestURI = CreateTopClansByVictoryPointsRequestURI(time, language, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGResponse<List<Clan>>>(output);

            return wgRawResponse;
        }

        private string CreateTopClansByVictoryPointsRequestURI(WGTimeDelta time, WGLanguageField language, string responseFields)
        {
            var target = "clan/top";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (time == WGTimeDelta.Step)
                sb.AppendFormat("&time={0}", "current_step");
            else
                sb.AppendFormat("&time={0}", "current_season");

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Top Clans by Victory Points

        #region Clan's Provinces

        /// <summary>
        /// Method returns list of clan's provinces.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        public IWGResponse<List<Province>> GetClansProvinces(long clanId)
        {
            return GetClansProvinces(clanId, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns list of clan's provinces.
        /// </summary>
        /// <param name="clanIds">clan id</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Province>> GetClansProvinces(long clanId, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClansProvincesRequestURI(clanId, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            // get the raw response
            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            // this is the response we will return
            var obj = new WGResponse<List<Province>>()
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Province>()
            };

            // any errors? stop what we're doing and return the object
            if (wgRawResponse.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            // nasty parsing, again :(
            if (jObject.HasValues)
                foreach (var province in jObject.Children())
                {
                    var provinceJsonString = province.First;

                    var provinceObj = provinceJsonString.ToObject<Province>();

                    obj.Data.Add(provinceObj);
                }

            return obj;
        }

        private string CreateClansProvincesRequestURI(long clanId, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "clan/provinces";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&clan_id={0}", clanId);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Clan's Provinces

        #region Clan's Victory Points

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanId">clan id</param>
        /// <returns></returns>
        [Obsolete("Warning. This method is deprecated and will be removed soon.")]
        public IWGResponse<List<long>> GetClansVictoryPoints(long clanId)
        {
            return GetClansVictoryPoints(new long[] { clanId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <returns></returns>
        [Obsolete("Warning. This method is deprecated and will be removed soon.")]
        public IWGResponse<List<long>> GetClansVictoryPoints(long[] clanIds)
        {
            return GetClansVictoryPoints(clanIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns number of clan victory points.
        /// </summary>
        /// <param name="clanIds">list of clan ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        [Obsolete("Warning. This method is deprecated and will be removed soon.")]
        public IWGResponse<List<long>> GetClansVictoryPoints(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClansVictoryPointsRequestURI(clanIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<long>>
            {
                Status = wgRawResponse.Status,
                Data = new List<long>()
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var clanId in clanIds)
            {
                var clanIdString = clanId.ToString();

                var victoryPoints = jObject[clanIdString]["points"].ToObject<long>();

                obj.Data.Add(victoryPoints);
            }

            return obj;
        }

        private string CreateClansVictoryPointsRequestURI(long[] clanIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "clan/victorypoints";

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

        #endregion Clan's Victory Points

        #region Clan Member Details

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanId">member id</param>
        /// <returns></returns>
        public IWGResponse<List<Member>> GetClanMemberInfo(long memberId)
        {
            return GetClanMemberInfo(new long[] { memberId }, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanIds">list of clan member ids</param>
        /// <returns></returns>
        public IWGResponse<List<Member>> GetClanMemberInfo(long[] memberIds)
        {
            return GetClanMemberInfo(memberIds, WGLanguageField.EN, null, null);
        }

        /// <summary>
        /// Method returns clan member info.
        /// </summary>
        /// <param name="clanIds">list of clan member ids</param>
        /// <param name="language">language</param>
        /// <param name="accessToken">access token</param>
        /// <param name="responseFields">fields to be returned. Null or string.Empty for all</param>
        /// <returns></returns>
        public IWGResponse<List<Member>> GetClanMemberInfo(long[] memberIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var requestURI = CreateClansVictoryPointsRequestURI(memberIds, language, accessToken, responseFields);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<Member>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Member>()
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var memberId in memberIds)
            {
                var memberIdString = memberId.ToString();

                var member = jObject[memberIdString].First.ToObject<Member>();

                obj.Data.Add(member);
            }

            return obj;
        }

        private string CreateClanMemberInfoRequestURI(long[] memberIds, WGLanguageField language, string accessToken, string responseFields)
        {
            var target = "clan/membersinfo";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&member_id={0}", string.Join(",", memberIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Clan Member Details

        #endregion Clans

        #region Encyclopedia

        #region List Vehicles

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>> GetAllVehicles()
        {
            return GetAllVehicles(WGLanguageField.EN, null);
        }

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>> GetAllVehicles(WGLanguageField language, string responseFields)
        {
            var requestURI = CreateAllVehiclesRequestURI(language, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var tankJObject in jObject.Children())
            {
                var tank = tankJObject.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>();

                obj.Data.Add(tank);
            }

            return obj;
        }

        private string CreateAllVehiclesRequestURI(WGLanguageField language, string responseFields)
        {
            var target = "encyclopedia/tanks";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion List Vehicles

        #region Vehicle Details

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tankId"></param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>> GetVehicleDetails(long tankId)
        {
            return GetVehicleDetails(new[] { tankId }, WGLanguageField.EN, null);
        }

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>> GetVehicleDetails(long[] tankIds)
        {
            return GetVehicleDetails(tankIds, WGLanguageField.EN, null);
        }

        /// <summary>
        /// Method returns list of all vehicles from Tankopedia.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>> GetVehicleDetails(long[] tankIds, WGLanguageField language, string responseFields)
        {
            var requestURI = CreateVehicleDetailssRequestURI(tankIds, language, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var tankJObject in jObject.Children())
            {
                var tank = tankJObject.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Tank>();

                obj.Data.Add(tank);
            }

            return obj;
        }

        private string CreateVehicleDetailssRequestURI(long[] tankIds, WGLanguageField language, string responseFields)
        {
            var target = "encyclopedia/tankinfo";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            sb.AppendFormat("&tank_id={0}", string.Join(",", tankIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Vehicle Details

        #region Engines

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>> GetEngines()
        {
            return GetEngines(new long[0], WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>> GetEngines(long moduleId)
        {
            return GetEngines(new[] { moduleId });
        }

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>> GetEngines(long[] moduleIds)
        {
            return GetEngines(moduleIds, WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of engines.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>> GetEngines(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var requestURI = CreateEnginesRequestURI(moduleIds, language, nation, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var engineJsonString in jObject.Children())
            {
                var engine = engineJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Engine>();

                obj.Data.Add(engine);
            }

            return obj;
        }

        private string CreateEnginesRequestURI(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var target = "encyclopedia/tankengines";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (nation != WGNation.All)
                sb.AppendFormat("&nation={0}", Enum.GetName(typeof(WGNation), nation).ToLowerInvariant());

            if (moduleIds.Length > 0)
                sb.AppendFormat("&module_id={0}", string.Join(",", moduleIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Engines

        #region Turrets

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>> GetTurrets()
        {
            return GetTurrets(new long[0], WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>> GetTurrets(long moduleId)
        {
            return GetTurrets(new[] { moduleId });
        }

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>> GetTurrets(long[] moduleIds)
        {
            return GetTurrets(moduleIds, WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of turrets.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>> GetTurrets(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var requestURI = CreateTurretsRequestURI(moduleIds, language, nation, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var turretJsonString in jObject.Children())
            {
                var turret = turretJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Turret>();

                obj.Data.Add(turret);
            }

            return obj;
        }

        private string CreateTurretsRequestURI(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var target = "encyclopedia/tankturrets";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (nation != WGNation.All)
                sb.AppendFormat("&nation={0}", Enum.GetName(typeof(WGNation), nation).ToLowerInvariant());

            if (moduleIds.Length > 0)
                sb.AppendFormat("&module_id={0}", string.Join(",", moduleIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Turrets

        #region Radios

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>> GetRadios()
        {
            return GetRadios(new long[0], WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>> GetRadios(long moduleId)
        {
            return GetRadios(new[] { moduleId });
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>> GetRadios(long[] moduleIds)
        {
            return GetRadios(moduleIds, WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>> GetRadios(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var requestURI = CreateRadiosRequestURI(moduleIds, language, nation, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var radioJsonString in jObject.Children())
            {
                var radio = radioJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Radio>();

                obj.Data.Add(radio);
            }

            return obj;
        }

        private string CreateRadiosRequestURI(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var target = "encyclopedia/tankradios";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (nation != WGNation.All)
                sb.AppendFormat("&nation={0}", Enum.GetName(typeof(WGNation), nation).ToLowerInvariant());

            if (moduleIds.Length > 0)
                sb.AppendFormat("&module_id={0}", string.Join(",", moduleIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Radios

        #region Suspensions

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>> GetSuspensions()
        {
            return GetSuspensions(new long[0], WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>> GetSuspensions(long moduleId)
        {
            return GetSuspensions(new[] { moduleId });
        }

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>> GetSuspensions(long[] moduleIds)
        {
            return GetSuspensions(moduleIds, WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of suspensions.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>> GetSuspensions(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var requestURI = CreateSuspensionsRequestURI(moduleIds, language, nation, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var suspensionJsonString in jObject.Children())
            {
                var suspension = suspensionJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Chassis>();

                obj.Data.Add(suspension);
            }

            return obj;
        }

        private string CreateSuspensionsRequestURI(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var target = "encyclopedia/tankchassis";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (nation != WGNation.All)
                sb.AppendFormat("&nation={0}", Enum.GetName(typeof(WGNation), nation).ToLowerInvariant());

            if (moduleIds.Length > 0)
                sb.AppendFormat("&module_id={0}", string.Join(",", moduleIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Suspensions

        #region Guns

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>> GetGuns()
        {
            return GetGuns(new long[0], WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">module id - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>> GetGuns(long moduleId)
        {
            return GetGuns(new[] { moduleId });
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>> GetGuns(long[] moduleIds)
        {
            return GetGuns(moduleIds, WGLanguageField.EN, WGNation.All, null);
        }

        /// <summary>
        /// Method returns list of radios.
        /// </summary>
        /// <param name="moduleIds">list of modules - not mandatory</param>
        /// <param name="language">language</param>
        /// <param name="nation">nation</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>> GetGuns(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var requestURI = CreateGunsRequestURI(moduleIds, language, nation, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var gubJsonString in jObject.Children())
            {
                var gun = gubJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Modules.Gun>();

                obj.Data.Add(gun);
            }

            return obj;
        }

        private string CreateGunsRequestURI(long[] moduleIds, WGLanguageField language, WGNation nation, string responseFields)
        {
            var target = "encyclopedia/tankguns";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (nation != WGNation.All)
                sb.AppendFormat("&nation={0}", Enum.GetName(typeof(WGNation), nation).ToLowerInvariant());

            if (moduleIds.Length > 0)
                sb.AppendFormat("&module_id={0}", string.Join(",", moduleIds));

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Guns

        #region Achievements

        /// <summary>
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements.TankAchievement>> GetAchievements()
        {
            return GetAchievements(WGLanguageField.EN, null);
        }

        /// <summary>
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned.</param>
        /// <returns></returns>
        public IWGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements.TankAchievement>> GetAchievements(WGLanguageField language, string responseFields)
        {
            var requestURI = CreateAchievementsRequestURI(language, responseFields);

            var output = this.GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements.TankAchievement>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements.TankAchievement>(),
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            foreach (var achievementJsonString in jObject.Children())
            {
                var achievement = achievementJsonString.First.ToObject<WGSharpAPI.Entities.EncyclopediaDetails.WorldOfTanks.Achievements.TankAchievement>();

                obj.Data.Add(achievement);
            }

            return obj;
        }

        private string CreateAchievementsRequestURI(WGLanguageField language, string responseFields)
        {
            var target = "encyclopedia/achievements";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            var requestURI = sb.ToString();

            return requestURI;
        }

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
        public IWGResponse<List<Tank>> GetTankStats(long accountId)
        {
            return GetTankStats(accountId, new long[0], WGLanguageField.EN, null, null, null);
        }

        /// <summary>
        /// Method returns overall statistics, Tank Company statistics, and clan statistics per each vehicle for a user.
        /// Warning. This method runs in test mode.
        /// </summary>
        /// <param name="accountId">account id</param>
        /// <param name="tankIds">list of player vehicle ids</param>
        /// <param name="language">language</param>
        /// <param name="responseFields">fields to be returned</param>
        /// <param name="accessToken">access token</param>
        /// <param name="inGarage">Filter by vehicle availability in the Garage. If the parameter is not specified, all vehicles are returned. Valid values: "1" — Return vehicles available in the Garage. "0" — Return vehicles that are no longer in the Garage.</param>
        /// <returns></returns>
        public IWGResponse<List<Tank>> GetTankStats(long accountId, long[] tankIds, WGLanguageField language, string responseFields, string accessToken, bool? inGarage)
        {
            var requestURI = CreateTankStatsRequestURI(accountId, tankIds, language, accessToken, responseFields, inGarage);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<List<Tank>>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new List<Tank>()
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            var accountIdString = accountId.ToString();

            var tankStats = jObject[accountIdString].Children();

            foreach (var tankStatJObj in tankStats)
            {
                var tankStat = tankStatJObj.ToObject<Tank>();

                obj.Data.Add(tankStat);
            }

            return obj;
        }

        private string CreateTankStatsRequestURI(long accountId, long[] tankIds, WGLanguageField language, string accessToken, string responseFields, bool? inGarage)
        {
            var target = "tanks/stats";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", accountId);

            if (tankIds.Length > 0)
                sb.AppendFormat("&tank_id={0}", string.Join(",", tankIds));

            if (inGarage.HasValue)
                sb.AppendFormat("&in_garage={0}", inGarage.Value ? 1 : 0);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Vehicle statistics

        #region Vehicle achievements

        public IWGResponse<Player> GetTankAchievements(long accountId)
        {
            return GetTankAchievements(accountId, new long[0], WGLanguageField.EN, null, null, null);
        }

        public IWGResponse<Player> GetTankAchievements(long accountId, long[] tankIds, WGLanguageField language, string responseFields, string accessToken, bool? inGarage)
        {
            var requestURI = CreateTankAchievementsRequestURI(accountId, tankIds, language, accessToken, responseFields, inGarage);

            var output = GetRequestResponse(requestURI);

            var wgRawResponse = JsonConvert.DeserializeObject<WGRawResponse>(output);

            var obj = new WGResponse<Player>
            {
                Status = wgRawResponse.Status,
                Meta = wgRawResponse.Meta,
                Data = new Player { AccountId = accountId }
            };

            if (obj.Status != "ok")
                return obj;

            var jObject = wgRawResponse.Data as JObject;

            var accountIdString = accountId.ToString();

            var tanks = jObject[accountIdString].Children();

            foreach (var jObjTank in tanks)
            {
                var tank = jObjTank.ToObject<Tank>();
                tank.Player = obj.Data;

                foreach (var jObjAchievement in jObjTank["achievements"].Children())
                {
                    var jProp = (JProperty)jObjAchievement;
                    var achievement = new Achievement { Name = jProp.Name, TimesAchieved = jObjAchievement.ToObject<long>() };

                    tank.Achievements.Add(achievement);
                }

                obj.Data.Tanks.Add(tank);
            }

            return obj;
        }

        private string CreateTankAchievementsRequestURI(long accountId, long[] tankIds, WGLanguageField language, string accessToken, string responseFields, bool? inGarage)
        {
            var target = "tanks/achievements";

            var generalUri = GetGeneralUri(target, language);

            var sb = new StringBuilder(generalUri);

            if (!string.IsNullOrWhiteSpace(responseFields))
                sb.AppendFormat("&fields={0}", responseFields);

            if (!string.IsNullOrWhiteSpace(accessToken))
                sb.AppendFormat("&access_token={0}", accessToken);

            sb.AppendFormat("&account_id={0}", accountId);

            if (tankIds.Length > 0)
                sb.AppendFormat("&tank_id={0}", string.Join(",", tankIds));

            if (inGarage.HasValue)
                sb.AppendFormat("&in_garage={0}", inGarage.Value ? 1 : 0);

            var requestURI = sb.ToString();

            return requestURI;
        }

        #endregion Vehicle achievements

        #endregion Player's vehicles

        #region General methods

        private string GetGeneralUri(string target, WGLanguageField language)
        {
            var languageField = EnumHelper<WGLanguageField>.GetEnumDescription(language);

            var sb = new StringBuilder();

            sb.Append(EnumHelper<WGRequestProtocol>.GetEnumDescription(_settings.RequestProtocol));

            sb.Append(_settings.BaseApiUri);
            if (!_settings.BaseApiUri.EndsWith("/"))
            {
                sb.Append('/');
            }

            sb.Append($"{target}/?application_id={_applicationId}&language={languageField}");

            var result = sb.ToString();

            return result;
        }

        private string GetRequestResponse(string requestURI)
        {
            Request.RequestURI = requestURI;
            return Request.GetResponse();
        }

        #endregion
    }
}