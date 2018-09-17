using System.Collections.Generic;

namespace VkBot.Manager.Services.Models
{
    public class IntentInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IntentSentenceInputModel> Sentences { get; set; }
    }
}