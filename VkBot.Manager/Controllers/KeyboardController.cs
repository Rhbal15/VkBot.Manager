using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.KeyboardViewModels;
using VkBot.Manager.ViewModels.StickerSetViewModels;

namespace VkBot.Manager.Controllers
{
    [Authorize]
    public class KeyboardController : Controller
    {
        private readonly IKeyboardService _keyboardService;
        private readonly IEmojiService _emojiService;
        private readonly IStickerService _stickerService;

        public KeyboardController(IKeyboardService keyboardService, IEmojiService emojiService,
            IStickerService stickerService)
        {
            _keyboardService = keyboardService;
            _emojiService = emojiService;
            _stickerService = stickerService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var keyboard = _keyboardService.GetActiveKeyboard();

            var keyboardButtonModels = keyboard.Buttons.OrderBy(p => p.Postition).Select(p =>
                new ButtonKeyboardIndexViewModel
                {
                    Symbol = p.Emoji.Symbol,
                    Id = p.Emoji.Id,
                    Position = p.Postition,
                    Mentions = _stickerService.GetStickerCountByEmoji(p.Emoji.Symbol)
                });

            var model = new KeyboardIndexViewModel
            {
                Buttons = keyboardButtonModels
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IFormCollection collection)
        {
            return RedirectToAction("Edit", new {id = collection["index"]});
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var currentEmoji = _emojiService.Get(id);

            var model = new KeyboardDetailsModel
            {
                Stickers = _stickerService.GetStickersByEmoji(currentEmoji.Symbol).Select(p => new StickerDetailModel
                {
                    Id = p.Id,
                    VkPhotoUrl = p.AzureImageUrl,
                    Emojis = string.Join(" ", p.Emoji.Select(e => e.Emoji.Symbol))
                })
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Update(IFormCollection collection)
        {
            _keyboardService.CreateAndActivateKeyboard(collection["smileSequence"]);

            return RedirectToAction("Index");
        }
    }
}