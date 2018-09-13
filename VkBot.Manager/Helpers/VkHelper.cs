namespace VkBot.Manager.Helpers
{
    public class VkHelper : IVkHelper
    {
        private readonly IConfigurationHelperService _configurationHelperService;

        public VkHelper(IConfigurationHelperService configurationHelperService)
        {
            _configurationHelperService = configurationHelperService;
        }

        public string GetAlbumUrl(long? vkAlumbId)
        {
            return vkAlumbId == null ? "" : $@"https://vk.com/{_configurationHelperService.GetGroupName()}?z=album-{_configurationHelperService.GetGroupId()}_{vkAlumbId}";
        }
    }
}
