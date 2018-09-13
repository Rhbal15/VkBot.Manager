using System.Collections.Generic;

namespace VkBot.Manager.ViewModels.StickersViewModels
{
    public class StickersEditModel
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public string StickerSetTitle { get; set; }
        public int StickerSetId { get; set; }
        public IEnumerable<EmojiStickersEditModel> AddedEmoji { get; set; }
        public IEnumerable<EmojiStickersEditModel> OtherEmoji { get; set; }
        public string CreatedDate { get; set; }
        public IEnumerable<EmojiStickersEditModel> KeyboardEmoji { get; set; }
    }
}
