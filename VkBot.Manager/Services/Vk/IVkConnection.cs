using VkBot.Manager.Helpers;
using VkNet;

namespace VkBot.Manager.Services.Vk
{
    public interface IVkConnection
    {
        VkApi GetVkApi(IConfigurationHelperService configurationHelper);
    }
}