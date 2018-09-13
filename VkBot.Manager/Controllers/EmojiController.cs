using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Services.Models;

namespace VkBot.Manager.Controllers
{
    public class EmojiController : Controller
    {
        private readonly IEmojiService _emojiService;

        public EmojiController(IEmojiService emojiService)
        {
            _emojiService = emojiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string emojiSequence)
        {
            foreach (var emoji in emojiSequence.Split(" "))
            {
                _emojiService.GetBySymbol(emoji);
            }

            return RedirectToAction("Index");
        }
    }
}