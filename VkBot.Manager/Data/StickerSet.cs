using System;
using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class StickerSet
    {
        public int Id { get; set; }
        public long? VkAlbumId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public StickerSetStatus StickerSetStatus { get; set; }

        public bool IsPublished { get; set; }

        public virtual IEnumerable<Sticker> Stickers { get; set; }
    }

    public enum StickerSetStatus
    {
        Unpublished,
        Published
    }
}