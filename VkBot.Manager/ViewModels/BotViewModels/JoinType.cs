using System.Runtime.Serialization;

namespace VkBot.Manager.ViewModels.BotViewModels
{
    public enum JoinType
    {
        [EnumMember(Value = "join")] Join,
        Leave
    }
}