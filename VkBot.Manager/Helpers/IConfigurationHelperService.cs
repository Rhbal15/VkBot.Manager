namespace VkBot.Manager.Helpers
{
    public interface IConfigurationHelperService
    {
        long? GetGroupId();
        ulong GetApplicationId();
        string GetVkLogin();
        string GetVkPassword();
        string GetStickerTempDirectory();
        string GetAccessToken();
        string GetCallbackConfirmationString();
        string GetGroupName();
    }
}
