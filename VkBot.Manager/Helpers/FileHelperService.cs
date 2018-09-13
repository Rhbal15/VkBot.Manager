using System.IO;
using Telegram.Bot.Types;

namespace VkBot.Manager.Helpers
{
    public class FileHelperService : IFileHelperService
    {
        private readonly IConfigurationHelperService _configurationHelper;

        public FileHelperService(IConfigurationHelperService configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public string CreateStickerSetTempDirectory(string stickerSetName)
        {
            var path = Path.Combine(_configurationHelper.GetStickerTempDirectory(), stickerSetName);
            Directory.CreateDirectory(path);
            return path;
        }

        public string GetFullPathToStickerFile(Sticker sticker)
        {
            return Path.Combine(_configurationHelper.GetStickerTempDirectory(), sticker.SetName,
                $"{sticker.FileId}.webp");
        }
    }
}