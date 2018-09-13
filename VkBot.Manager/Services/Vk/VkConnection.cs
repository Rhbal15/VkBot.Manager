using VkBot.Manager.Helpers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace VkBot.Manager.Services.Vk
{
    public class VkConnection : IVkConnection
    {
        private VkApi _api;

        public VkApi GetVkApi(IConfigurationHelperService configurationHelper)
        {
            if (_api != null) return _api;

            _api = new VkApi();
            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = configurationHelper.GetApplicationId(),
                Login = configurationHelper.GetVkLogin(),
                Password = configurationHelper.GetVkPassword(),
                Settings = Settings.Photos
            });

            return _api;
        }
    }
}