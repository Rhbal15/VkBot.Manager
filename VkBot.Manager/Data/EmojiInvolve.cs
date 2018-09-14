using System;

namespace VkBot.Manager.Data
{
    /// <summary>
    /// Хранит смайлы, которые присылал пользователь.
    /// </summary>
    public class EmojiInvolve
    {
        public int Id { get; set; }

        public DateTime InvolveDate { get; set; }

        public BotUser BotUser { get; set; }
        public Emoji Emoji { get; set; }
    }
}