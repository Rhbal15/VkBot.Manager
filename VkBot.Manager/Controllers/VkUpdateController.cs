using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Data;
using VkBot.Manager.Helpers;
using VkBot.Manager.Services.Models;
using VkBot.Manager.Services.Vk;
using VkBot.Manager.ViewModels.BotViewModels;

namespace VkBot.Manager.Controllers
{
    [Route("api/[controller]")]
    public class VkUpdateController : Controller
    {
        private readonly IVkGroupMessageService _vkGroupMessageService;
        private readonly IConfigurationHelperService _configurationHelper;
        private readonly IStickerService _stickerService;
        private readonly IEmojiService _emojiService;
        private readonly IKeyboardService _keyboardService;
        private readonly IBotUserService _botUserService;
        private readonly IReceivedMessageService _receivedMessageService;

        public VkUpdateController(
            IVkGroupMessageService vkGroupMessageService,
            IConfigurationHelperService configurationHelper,
            IStickerService stickerService,
            IEmojiService emojiService,
            IKeyboardService keyboardService,
            IBotUserService botUserService,
            IReceivedMessageService receivedMessageService)
        {
            _vkGroupMessageService = vkGroupMessageService;
            _configurationHelper = configurationHelper;
            _stickerService = stickerService;
            _emojiService = emojiService;
            _keyboardService = keyboardService;
            _botUserService = botUserService;
            _receivedMessageService = receivedMessageService;
        }

        [HttpPost]
        public string Post([FromBody] VkCallbackUpdateModel update)
        {
            switch (update.Type)
            {
                case VkUpdateType.Confirmation:
                    return _configurationHelper.GetCallbackConfirmationString();
                case VkUpdateType.GroupJoin:
                    _botUserService.CreateSubscription(update.Object.UserId, JoinType.Join);
                    break;
                case VkUpdateType.GroupLeave:
                    _botUserService.CreateSubscription(update.Object.UserId, JoinType.Leave);
                    break;
                case VkUpdateType.MessageNew:

                    if (_receivedMessageService.IsReceived(update.Object.UserId, update.Object.Id))
                    {
                        return "ok";
                    }

                    _receivedMessageService.Create(new ReceivedMessage
                    {
                        MessageId = update.Object.Id,
                        BotUser = _botUserService.Get(update.Object.UserId)
                    });

                    var body = update.Object.Body;

                    var keyboardLabels = _keyboardService.GetActiveKeyboard().Buttons.OrderBy(p => p.Postition)
                        .Select(p => p.Emoji.Symbol)
                        .ToList();

                    if (_receivedMessageService.IsFirstMessage(update.Object.UserId, update.Object.Id))
                    {
                        _vkGroupMessageService.SendMessage(
                            userId: update.Object.UserId,
                            message: "Кстати, с этим сообщением я отправил тебе клавиатуру.\n\n " +
                                     "Ты можешь её использовать, чтобы получать новые стикеры одним нажатием.\n\n" +
                                     "Для всех смайлов на клавиатуре у меня есть стикеры 😉",
                            keyboardLabels: keyboardLabels);
                    }

                    if (_emojiService.IsExist(body))
                    {
                        var sticker = _stickerService.GetStickerByEmoji(body, update.Object.UserId);

                        if (sticker == null)
                        {
                            _vkGroupMessageService.SendMessage(update.Object.UserId,
                                "Вот это ты мне смайл прислал 😱\n\n" +
                                "Даже не знаю, какой стикер тебе отправить. \n\n " +
                                "Обязательно подберу для него стикер в будущем!",
                                keyboardLabels: keyboardLabels);
                        }
                        else
                        {
                            _vkGroupMessageService.SendMessage(userId: update.Object.UserId,
                                photoId: sticker.VkImageId, keyboardLabels: keyboardLabels);
                        }

                        return "Ok";
                    }

                    _vkGroupMessageService.SendMessage(update.Object.UserId,
                        "Даже не знаю, что тебе на это ответить 😔\n\n" +
                        "Пока я могу отвечать только на смайлы. Попробуешь использовать клавиатуру, которую я прислал? \n\n" +
                        "Тогда я точно смогу прислать тебе ответ.",
                        keyboardLabels: keyboardLabels);

                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            return "ok";
        }
    }

    public static class StringHelper
    {
        public static bool EqualsIgnoreCase(this string first, string second)
        {
            return string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
        }
    }
}