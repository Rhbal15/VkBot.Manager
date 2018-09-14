using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Services.Models;

namespace VkBot.Manager.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер для работы с аналатикой группы и бота. Просмотра пользователей группы, подписок и отписок, сообщений, на которые бот не смог ответить.
    /// </summary>
    public class VkUserController : Controller
    {
        private readonly IBotUserService _botUserService;

        public VkUserController(IBotUserService botUserService)
        {
            _botUserService = botUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Контроллер, который предоставляет доступ к странице, на которой отображаются подписки и отписки от группы вк.
        /// </summary>
        /// <returns>Представление страницы всех подписок и отписок</returns>
        public IActionResult Subscriptions()
        {
            var subscriptions = _botUserService.GetSubscriptions();

            var model = new SubscriptionsVkUserViewModel
            {
                Subscriptions = subscriptions.Select(p => new SubscriptionsVkUserListViewModel
                {
                    Id = p.Id,
                    VkId = p.BotUser.VkId,
                    UserFirstName = p.BotUser.FirstName,
                    UserLastName = p.BotUser.LastName,
                    JoinDate = p.JoinDate.ToString(CultureInfo.CurrentCulture),
                    JoinType = p.JoinType,
                    VkUrl = $@"https://vk.com/id{p.BotUser.VkId}"
                })
            };

            return View(model);
        }

        public IActionResult Messages()
        {
            return View();
        }
    }
}