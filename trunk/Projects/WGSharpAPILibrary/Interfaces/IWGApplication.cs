using WGSharpAPI.Entities;
using WGSharpAPI.Enums;
using System.Collections.Generic;

namespace WGSharpAPI.Interfaces
{
    public interface IWGApplication
    {
        WGResponse<List<Player>> SearchPlayers(string searchTerm);
        //WGResponse<Player> SearchPlayers(string searchTerm, int limit);
        //WGResponse<Player> SearchPlayers(string searchTerm, WGPlayerListField responseFields, WGLanguageField language, int limit);

        WGResponse<List<Player>> GetPlayerInfo(long accountId);
        WGResponse<List<Player>> GetPlayerInfo(long[] accountIds);
        //WGResponse<List<Player>> GetPlayerInfo(long[] accountIds, WGLanguageField language, string accessToken, string responseFields);
    }
}
