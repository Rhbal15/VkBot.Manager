using System;
using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class Intent
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

        public IEnumerable<IntentSentence> IntentSentences { get; set; }
    }
}