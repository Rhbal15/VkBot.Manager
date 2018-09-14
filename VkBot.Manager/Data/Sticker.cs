using System;
using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class Sticker
    {
        public int Id { get; set; }
        public long? VkImageId { get; set; }
        public string TelegramFileId { get; set; }
        public string AzureImageUrl { get; set; }

        public StickerStatus StickerStatus { get; set; }

        public IEnumerable<StickerEmoji> Emoji { get; set; }
        public virtual StickerSet StickerSet { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<ShowedSticker> ShowedStickers { get; set; }
    }

    public enum StickerStatus
    {
        Unpublished,
        Published
    }
}