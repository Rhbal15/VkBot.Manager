using System.Collections.Generic;
using VkBot.Manager.Controllers;

namespace VkBot.Manager.ViewModels.IntentViewModels
{
    public class IndexIntentViewModel
    {
        public IEnumerable<IndexIntentListViewModel> Intents { get; set; }
    }
}
