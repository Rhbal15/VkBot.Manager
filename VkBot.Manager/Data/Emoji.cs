using System.Collections.Generic;

namespace VkBot.Manager.Data
{
    public class Emoji
    {
        public int Id { get; set; }

        public string Symbol { get; set; }
        public EmojiGroup EmojiGroup { get; set; }

        public virtual IEnumerable<StickerEmoji> Stickers { get; set; }
        public virtual IEnumerable<KeyboardButton> KeyboardButtons { get; set; }
        public IEnumerable<EmojiDescription> EmojiDescriptions { get; set; }
        public IEnumerable<EmojiInvolve> EmojiInvolves { get; set; }
    }
}