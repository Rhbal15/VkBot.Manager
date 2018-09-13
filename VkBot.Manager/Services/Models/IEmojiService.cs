using System.Collections.Generic;
using VkBot.Manager.Models;

namespace VkBot.Manager.Services.Models
{
    public interface IEmojiService
    {
        IEnumerable<Emoji> GetAll();
        Emoji Get(int id);
        Emoji GetBySymbol(string symbol);
        bool IsExist(string symbol);
        IEnumerable<Emoji> GetAvaliableStickerSetEmojis(int stickerSetId);
    }
}