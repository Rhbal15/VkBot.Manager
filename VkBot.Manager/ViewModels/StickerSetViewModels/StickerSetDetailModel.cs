using System.Collections.Generic;

namespace VkBot.Manager.ViewModels.StickerSetViewModels
{
    public class StickerSetDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedDate { get; set; }
        public long? VkAlbumId { get; set; }
        public string VkAlbumUrl { get; set; }
        public IEnumerable<StickerDetailModel> Stickers { get; set; }
        public int StickerCount { get; set; }
        public bool IsPublished { get; set; }
    }
}
