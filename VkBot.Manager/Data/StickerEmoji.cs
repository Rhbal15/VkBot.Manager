namespace VkBot.Manager.Models
{
    public class StickerEmoji
    {
        public int Id { get; set; }

        public Sticker Sticker { get; set; }
        public Emoji Emoji { get; set; }
    }
}