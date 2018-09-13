namespace VkBot.Manager.Models
{
    public class KeyboardButton
    {
        public int Id { get; set; }
        public int Postition { get; set; }
        public Keyboard Keyboard { get; set; }

        public Emoji Emoji { get; set; }
    }
}