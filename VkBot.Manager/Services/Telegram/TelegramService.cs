using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;

namespace VkBot.Manager.Services.Telegram
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;

        public TelegramService(IConfiguration configuration)
        {
            _botClient = new TelegramBotClient(configuration.GetSection("TelegramParams")["AccessToken"]);
        }

        public Task<StickerSet> GetStickerSetAsync(string stickerSetName)
        {
            return _botClient.GetStickerSetAsync(stickerSetName);
        }

        public async Task<File> DownloadSticker(string stickerFileId, Stream stream)
        {
            return await _botClient.GetInfoAndDownloadFileAsync(stickerFileId, stream);
        }
    }
}