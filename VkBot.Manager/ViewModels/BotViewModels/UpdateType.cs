using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VkBot.Manager.ViewModels.BotViewModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VkUpdateType
    {
        [EnumMember(Value = "confirmation")] Confirmation,
        [EnumMember(Value = "group_join")] GroupJoin,
        [EnumMember(Value = "message_new")] MessageNew
    }
}