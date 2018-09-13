using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace VkBot.Manager.Services.Telegram
{
    public interface ITelegramService
    {
        Task<StickerSet> GetStickerSetAsync(string stickerSetName);
        Task<File> DownloadSticker(string stickerFileId, Stream stream);
    }
}
