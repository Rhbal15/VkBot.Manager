using System.Collections.Generic;

namespace VkBot.Manager.Controllers
{
    public class SubscriptionsVkUserViewModel
    {
        public IEnumerable<SubscriptionsVkUserListViewModel> Subscriptions { get; set; }
    }
}