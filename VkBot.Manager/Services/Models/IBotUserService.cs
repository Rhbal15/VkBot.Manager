using System.Collections.Generic;
using VkBot.Manager.Data;
using VkBot.Manager.ViewModels.BotViewModels;
using BotUser = VkBot.Manager.Data.BotUser;

namespace VkBot.Manager.Services.Models
{
    public interface IBotUserService
    {
        BotUser Get(long vkUserId);
        BotUser Create(BotUser user);
        bool IsExist(long vkUserId);

        void CreateSubscription(long vkUserId, JoinType? joinType);
        /// <summary>
        /// Метод для получения всех подписок и отписок, которые были сохранены в систему.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Subscription> GetSubscriptions();
    }
}
