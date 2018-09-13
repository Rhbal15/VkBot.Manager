using System;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Helpers;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.BotViewModels;

namespace VkBot.Manager.Controllers
{
    [Route("api/[controller]")]
    public class SubscriberController : Controller
    {
        private readonly IConfigurationHelperService _configurationHelper;
        private readonly IBotUserService _botUserService;

        public SubscriberController(
            IConfigurationHelperService configurationHelper,
            IBotUserService botUserService)
        {
            _configurationHelper = configurationHelper;
            _botUserService = botUserService;
        }

        [HttpPost]
        public string Post([FromBody] VkCallbackUpdateModel update)
        {
            switch (update.Type)
            {
                case VkUpdateType.Confirmation:
                    return _configurationHelper.GetCallbackConfirmationString();
                case VkUpdateType.GroupJoin:
                    if (update.Object.JoinType != null)
                    {
                        _botUserService.CreateSubscription(update.Object.UserId, update.Object.JoinType);
                    }
                    break;
                case VkUpdateType.MessageNew:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return "ok";
        }
    }
}