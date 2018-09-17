using System;
using System.Collections.Generic;

namespace VkBot.Manager.ViewModels.IntentViewModels
{
    public class IntentDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<SentenceIntentDetailViewModel> Sentences { get; set; }
    }
}