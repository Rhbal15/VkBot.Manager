using System.Collections.Generic;
using VkBot.Manager.ViewModels.StickerSetViewModels;

namespace VkBot.Manager.ViewModels.KeyboardViewModels
{
    public class KeyboardDetailsModel
    {
        public IEnumerable<StickerDetailModel> Stickers { get; set; }
    }
}