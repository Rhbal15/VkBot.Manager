using VkBot.Manager.Data;

namespace VkBot.Manager.Models
{
    public class ReceivedMessage
    {
        public int Id { get; set; }
        public long MessageId { get; set; }
        public BotUser BotUser { get; set; }
    }
}