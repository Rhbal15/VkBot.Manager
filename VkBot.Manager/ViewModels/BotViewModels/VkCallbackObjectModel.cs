using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace VkBot.Manager.ViewModels.BotViewModels
{
    public class VkCallbackObjectModel
    {
        public long Id { get; set; }

        [JsonProperty("date"), JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        [JsonProperty("out")] public long Out { get; set; }
        [JsonProperty("user_id")] public long UserId { get; set; }
        [JsonProperty("read_state")] public long ReadState { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        [JsonProperty("join_type")] public JoinType? JoinType { get; set; }
    }
}