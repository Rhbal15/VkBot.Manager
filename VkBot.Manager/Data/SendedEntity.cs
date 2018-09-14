using System;

namespace VkBot.Manager.Data
{
    public class SendedEntity
    {
        public int Id { get; set; }

        public BotUser BotUser { get; set; }
        public DateTime SendedDate { get; set; }
    }
}