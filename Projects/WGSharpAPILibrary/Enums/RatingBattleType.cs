using System.ComponentModel;

namespace WGSharpAPI.Enums
{
    public enum RatingBattleType
    {
        [Description("company")]
        Company = 1,
        [Description("random")]
        Random = 2,
        [Description("team")]
        Team = 3,
        [Description("default")]
        Default = 4,
    }
}
