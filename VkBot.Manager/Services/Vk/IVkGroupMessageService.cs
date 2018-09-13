using System.Collections.Generic;

namespace VkBot.Manager.Services.Vk
{
    public interface IVkGroupMessageService
    {
        void SendMessage(long userId, string message = "", long? photoId = null,
            List<string> keyboardLabels = null);
    }
}