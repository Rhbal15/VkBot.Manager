using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VkBot.Manager.Helpers;
using VkNet;
using VkNet.Model;
using VkNet.Model.Keyboard;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace VkBot.Manager.Services.Vk
{
    public class VkGroupMessageService : IVkGroupMessageService
    {
        private readonly IVkGroupPhotoService _vkGroupPhotoService;
        private readonly VkApi _api;

        public VkGroupMessageService(IConfigurationHelperService configurationHelper,
            IVkGroupPhotoService vkGroupPhotoService)
        {
            _vkGroupPhotoService = vkGroupPhotoService;
            _api = new VkApi();
            _api.Authorize(new ApiAuthParams
            {
                AccessToken = configurationHelper.GetAccessToken()
            });
        }

        public void SendMessage(long userId, string message = "", long? photoId = null,
            List<string> keyboardLabels = null)
        {
            var keyboard =
                CreateKeyboard(keyboardLabels?.GetRange(0, keyboardLabels.Count < 36 ? keyboardLabels.Count : 36));

            _api.Messages.Send(new MessagesSendParams
            {
                UserId = userId,
                Message = message,
                Keyboard = keyboard,
                Attachments = _vkGroupPhotoService.Get(photoId)
            });
        }

        public MessageKeyboard CreateKeyboard(List<string> labels)
        {
            if (labels == null)
            {
                return null;
            }

            var buttons = new List<ReadOnlyCollection<MessageKeyboardButton>>();

            for (var i = 0; i < labels.Count; i += 4)
            {
                var countRange = labels.Count - i < 4 ? labels.Count - i : 4;
                var range = labels.GetRange(i, countRange);
                var keyboardRow = CreateMessageKeyboardRow(range);
                buttons.Add(keyboardRow);
            }

            buttons.Add(CreateMessageKeyboardRow(new List<string> {"Случайный стикер"}));

            return new MessageKeyboard
            {
                Buttons = buttons.ToReadOnlyCollection()
            };
        }

        public ReadOnlyCollection<MessageKeyboardButton> CreateMessageKeyboardRow(
            IEnumerable<string> labels)
        {
            return labels.Select(CreateMessageKeyboardButton).ToReadOnlyCollection();
        }

        public MessageKeyboardButton CreateMessageKeyboardButton(string label)
        {
            return new MessageKeyboardButton
            {
                Action = new MessageKeyboardButtonAction
                {
                    Label = label
                }
            };
        }
    }
}