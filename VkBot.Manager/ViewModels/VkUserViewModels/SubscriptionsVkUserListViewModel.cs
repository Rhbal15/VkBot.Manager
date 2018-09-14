using VkBot.Manager.ViewModels.BotViewModels;

namespace VkBot.Manager.Controllers
{
    public class SubscriptionsVkUserListViewModel
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName => UserFirstName + " " + UserLastName;
        public string JoinDate { get; set; }
        public JoinType? JoinType { get; set; }
        public string VkUrl { get; set; }
        public long VkId { get; set; }
    }
}