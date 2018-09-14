using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class BotUser
    {
        public int Id { get; set; }

        public long VkId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<ReceivedMessage> ReceivedMessages { get; set; }
        public IEnumerable<Subscription> Subscriptions { get; set; }
        public IEnumerable<EmojiInvolve> EmojiInvolves { get; set; }
    }
}