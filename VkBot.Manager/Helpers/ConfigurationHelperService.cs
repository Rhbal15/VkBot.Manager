using Microsoft.Extensions.Configuration;

namespace VkBot.Manager.Helpers
{
    public class ConfigurationHelperService : IConfigurationHelperService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationHelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public long? GetGroupId()
        {
            return _configuration.GetSection("VkParams").GetSection("GroupParams").GetValue<long?>("GroupId");
        }

        public ulong GetApplicationId()
        {
            return _configuration.GetSection("VkParams").GetSection("ApplicationLogin")
                .GetValue<ulong>("ApplicationId");
        }

        public string GetAccessToken()
        {
            return _configuration.GetSection("VkParams").GetSection("GroupParams")["AccessToken"];
        }

        public string GetCallbackConfirmationString()
        {
            return _configuration.GetSection("VkParams").GetSection("GroupParams")["CallbackConfirmationString"];
        }

        public string GetGroupName()
        {
            return _configuration.GetSection("VkParams").GetSection("GroupParams")["GroupName"];
        }

        public string GetVkLogin()
        {
            return _configuration.GetSection("VkParams").GetSection("ApplicationLogin")["AccounLogin"];
        }

        public string GetVkPassword()
        {
            return _configuration.GetSection("VkParams").GetSection("ApplicationLogin")["AccountPassword"];
        }

        public string GetStickerTempDirectory()
        {
            return _configuration["StickerTempDirectory"];
        }
    }
}