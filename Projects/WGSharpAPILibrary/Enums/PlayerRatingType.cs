using System.ComponentModel;

namespace WGSharpAPI.Enums
{
    public enum PlayerRatingType
    {
        [Description("1")]
        OneDay,
        [Description("7")]
        Week,
        [Description("28")]
        Month,
        [Description("all")]
        Any,
    }
}
