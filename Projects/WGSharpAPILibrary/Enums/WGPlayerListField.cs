using System;

namespace WGSharpAPI.Enums
{
    /// <summary>
    /// THIS WILL MOST PROBABLY BE REMOVED IN THE FUTURE
    /// </summary>
    [Flags]
    public enum WGPlayerListField
    {
        AccountId = 1,
        Nickname = 2,

        All = AccountId | Nickname,
    }
}
