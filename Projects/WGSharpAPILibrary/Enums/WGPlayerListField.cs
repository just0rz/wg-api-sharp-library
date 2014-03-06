using System;

namespace WGSharpAPI.Enums
{
    [Flags]
    public enum WGPlayerListField
    {
        AccountId = 1,
        Nickname = 2,

        All = AccountId | Nickname,
    }
}
