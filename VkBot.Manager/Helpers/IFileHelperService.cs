using Telegram.Bot.Types;

namespace VkBot.Manager.Helpers
{
    public interface IFileHelperService
    {
        string CreateStickerSetTempDirectory(string stickerSetName);
        string GetFullPathToStickerFile(Sticker sticker);
    }
}
