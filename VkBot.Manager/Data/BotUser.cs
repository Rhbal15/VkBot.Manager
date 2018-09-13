using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class BotUser
    {
        public int Id { get; set; }
        public long VkId { get; set; }
        public IEnumerable<Manager.Models.ReceivedMessage> ReceivedMessages { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}