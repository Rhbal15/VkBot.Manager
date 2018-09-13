namespace VkBot.Manager.ViewModels.StickerSetViewModels
{
    public class StickerSetIndexListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long? VkAlbumId { get; set; }
        public string CreatedDate { get; set; }
        public int SickerCount { get; set; }
        public string AvaliableEmoji { get; set; }
    }
}