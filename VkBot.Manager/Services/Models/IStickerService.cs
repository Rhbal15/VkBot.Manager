using System.Collections.Generic;
using System.Threading.Tasks;
using VkBot.Manager.Data;

namespace VkBot.Manager.Services.Models
{
    public interface IStickerService
    {
        Task<int> LoadStickerSet(string stickerSetName);
        StickerSet CreateStickerSet(string stickerSetTitle, string stickerSetName, long? albumId = null);

        Sticker CreateSticker(string stickerEmoji,
            string stickerFileId,
            long? photoId,
            int? stickerSetId = null,
            StickerStatus stickerStatus = StickerStatus.Unpublished,
            string azureImageUrl = "");

        IEnumerable<StickerSet> GetAllStickerSets();
        Sticker GetStickerByEmoji(string inputEmoji, long botUserId);
        StickerSet GetStickerSet(int id);
        void DeleteSticker(int id);
        Sticker GetSticker(int id);
        int AddEmoji(int stickerId, int emojiId);
        int RemoveEmoji(int stickerEmojiId);
        int GetStickerCountByEmoji(string symbol);
        IEnumerable<Sticker> GetStickersByEmoji(string symbol);
        Task PublishStickerSet(int stickerSetId);
        void DeleteStickerSet(int stickerSetId);
        Sticker GetRandomSticker();
    }
}