using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Helpers;
using VkBot.Manager.Services.Models;
using VkBot.Manager.Services.Vk;
using VkBot.Manager.ViewModels.StickerSetViewModels;

namespace VkBot.Manager.Controllers
{
    [Authorize]
    public class StickerSetController : Controller
    {
        private readonly IStickerService _stickerService;
        private readonly IEmojiService _emojiService;
        private readonly IVkHelper _vkHelper;

        public StickerSetController(IStickerService stickerService,
            IEmojiService emojiService,
            IVkHelper vkHelper,
            IVkGroupPhotoService vkGroupPhotoService)
        {
            _stickerService = stickerService;
            _emojiService = emojiService;
            _vkHelper = vkHelper;
        }
        
        public ActionResult Index()
        {
            var stickerSets = _stickerService.GetAllStickerSets();

            var model = new StickerSetIndexModel
            {
                StickerSets = stickerSets.Select(p => new StickerSetIndexListModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    VkAlbumId = p.VkAlbumId,
                    CreatedDate = p.CreatedDate.ToString(CultureInfo.CurrentCulture),
                    SickerCount = p.Stickers.Count(),
                    AvaliableEmoji = string.Join(" ",
                        _emojiService.GetAvaliableStickerSetEmojis(p.Id).Select(s => s.Symbol))
                })
            };

            return View(model);
        }

        // GET: StickerSet/Details/5
        public ActionResult Details(int id)
        {
            var stickerSet = _stickerService.GetStickerSet(id);

            var model = new StickerSetDetailModel
            {
                Id = stickerSet.Id,
                Title = stickerSet.Title,
                CreatedDate = stickerSet.CreatedDate.ToString(CultureInfo.CurrentCulture),
                VkAlbumId = stickerSet.VkAlbumId,
                VkAlbumUrl = _vkHelper.GetAlbumUrl(stickerSet.VkAlbumId),
                Stickers = stickerSet.Stickers.Select(p => new StickerDetailModel
                {
                    Id = p.Id,
                    VkPhotoUrl = p.AzureImageUrl,
                    Emojis = string.Join(" ", p.Emoji.Select(e => e.Emoji).Select(e => e.Symbol))
                }),
                StickerCount = stickerSet.Stickers.Count()
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Publish(int id)
        {
            await _stickerService.PublishStickerSet(id);

            return RedirectToAction("Details", new {id});
        }

        // GET: StickerSet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StickerSet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string stickerSetName, string json, bool publish)
        {
            var stickerSetId = await _stickerService.LoadStickerSet(stickerSetName);

            if (publish)
            {
                await _stickerService.PublishStickerSet(stickerSetId);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            _stickerService.DeleteStickerSet(id);

            return RedirectToAction("Index");
        }
    }
}