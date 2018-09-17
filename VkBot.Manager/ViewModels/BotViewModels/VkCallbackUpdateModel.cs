using Newtonsoft.Json;

namespace VkBot.Manager.ViewModels.BotViewModels
{
    public class VkCallbackUpdateModel
    {
        public VkUpdateType? Type { get; set; }
        public VkCallbackObjectModel Object { get; set; }
        [JsonProperty("group_id")] public int GroupId { get; set; }
    }
}