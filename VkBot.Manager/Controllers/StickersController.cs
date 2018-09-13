using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Services.Models;
using VkBot.Manager.Services.Vk;
using VkBot.Manager.ViewModels.StickersViewModels;

namespace VkBot.Manager.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class StickersController : Controller
    {
        private readonly IStickerService _stickerService;
        private readonly IEmojiService _emojiService;
        private readonly IKeyboardService _keyboardService;

        public StickersController(IStickerService stickerService,
            IEmojiService emojiService,
            IKeyboardService keyboardService)
        {
            _stickerService = stickerService;
            _emojiService = emojiService;
            _keyboardService = keyboardService;
        }

        [HttpPost]
        public int AddEmoji([FromBody] AddEmojiModel model)
        {
            return _stickerService.AddEmoji(model.StickerId, model.EmojiId);
        }

        [HttpDelete]
        public int RemoveEmoji(int id)
        {
            return _stickerService.RemoveEmoji(id);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var sticker = _stickerService.GetSticker(id);
            var addedEmoji = sticker.Emoji.Select(p => p.Emoji).ToList();
            var notAddedEmoji = _emojiService.GetAll().Except(addedEmoji).ToList();
            var keyboardEmoji = _keyboardService.GetActiveKeyboardEmojis();
            var notAddedKeyboardEmojis = notAddedEmoji.Intersect(keyboardEmoji).ToList();
            var otherEmojis = notAddedEmoji.Except(notAddedKeyboardEmojis);

            var model = new StickersEditModel
            {
                Id = sticker.Id,
                PhotoUrl = sticker.AzureImageUrl,
                StickerSetTitle = sticker.StickerSet.Title,
                CreatedDate = sticker.CreatedDate.ToString(CultureInfo.CurrentCulture),
                StickerSetId = sticker.StickerSet.Id,
                AddedEmoji = sticker.Emoji.Select(p => new EmojiStickersEditModel
                {
                    Id = p.Emoji.Id,
                    Symbol = p.Emoji.Symbol,
                    ConnectionId = p.Id
                }),
                KeyboardEmoji = notAddedKeyboardEmojis.Select(p => new EmojiStickersEditModel
                {
                    Id = p.Id,
                    Symbol = p.Symbol
                }),
                OtherEmoji = otherEmojis.Select(p => new EmojiStickersEditModel
                {
                    Id = p.Id,
                    Symbol = p.Symbol
                })
            };

            return View(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _stickerService.DeleteSticker(id);
        }
    }
}