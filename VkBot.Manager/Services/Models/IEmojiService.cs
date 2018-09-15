using System.Collections.Generic;
using VkBot.Manager.Data;
using VkBot.Manager.ViewModels.EmojisViewModels;

namespace VkBot.Manager.Services.Models
{
    public interface IEmojiService
    {
        IEnumerable<Emoji> GetAll();
        Emoji Get(int id);
        Emoji GetBySymbol(string symbol);
        IEnumerable<Emoji> GetEmojisByEmojiSequence(string emojiSequence);
        IEnumerable<Emoji> GetEmojisBySymbols(IEnumerable<string> symbols);

        bool IsExist(string symbol);
        IEnumerable<Emoji> GetAvaliableStickerSetEmojis(int stickerSetId);
        IEnumerable<EmojiGroup> GetGroups();
        void CreateGroup(CreateGroupEmojiInputModel inputModel);

        IEnumerable<string> SplitEmojiSequence(string emojiSequence);
    }
}