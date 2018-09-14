namespace VkBot.Manager.Data
{
    public class ShowedSticker
    {
        public int Id { get; set; }
        public Sticker Sticker { get; set; }
        public Emoji Emoji { get; set; }
        public BotUser BotUser { get; set; }
    }
}
