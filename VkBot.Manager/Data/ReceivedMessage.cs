namespace VkBot.Manager.Data
{
    public class ReceivedMessage
    {
        public int Id { get; set; }

        public long MessageId { get; set; }
        public BotUser BotUser { get; set; }
    }
}