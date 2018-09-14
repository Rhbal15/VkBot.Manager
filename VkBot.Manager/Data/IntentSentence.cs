using System;

namespace VkBot.Manager.Data
{
    public class IntentSentence
    {
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime CreateDate { get; set; }

        public Intent Intent { get; set; }
    }
}