using System.Collections.Generic;

namespace VkBot.Manager.Models
{
    public class Emoji
    {
        public int Id { get; set; }
        public string Symbol { get; set; }

        public virtual IEnumerable<StickerEmoji> Stickers { get; set; }
        public virtual IEnumerable<KeyboardButton> KeyboardButtons { get; set; }
    }
}