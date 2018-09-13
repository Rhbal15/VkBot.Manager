using VkBot.Manager.Models;

namespace VkBot.Manager.Services.Models
{
    public interface IReceivedMessageService
    {
        ReceivedMessage Create(ReceivedMessage message);
        bool IsReceived(long vkUserId, long vkMessageId);
        bool IsFirstMessage(long vkUserId, long vkMessageId);
    }
}

