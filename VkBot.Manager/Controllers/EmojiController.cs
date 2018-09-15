using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VkBot.Manager.Exceptions;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.EmojisViewModels;

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

        public IActionResult Group()
        {
            var groups = _emojiService.GetGroups();

            var groupList = groups.Select(p => new GroupListEmojisViewModel
            {
                Id = p.Id,
                Prioruty = p.Priority,
                Name = p.Name,
                CreatedDate = p.CreateDate.ToString(CultureInfo.CurrentCulture),
                Emojis = p.Emojis.Select(e => e.Symbol).Join(" ")
            });

            var model = new GroupEmojisViewModel
            {
                Groups = groupList
            };

            return View(model);
        }

        public IActionResult CreateGroup(ErrorCreateGroupInputModel inputModel)
        {
            return View(inputModel);
        }

        [HttpPost]
        public IActionResult CreateGroup(CreateGroupEmojiInputModel inputModel)
        {
            try
            {
                _emojiService.CreateGroup(inputModel);
            }
            catch (SuchGroupNameAlreadyExists)
            {
                return RedirectToAction("CreateGroup", new ErrorCreateGroupInputModel
                {
                    ErrotType = ErrorCreateGroupType.SuchGroupNameExists,
                    Name = inputModel.Name,
                    Priority = inputModel.Priority,
                    EmojiSequence = inputModel.EmojiSequence
                });
            }
            catch (SuchGroupPriorityAlreadyExists)
            {
                return RedirectToAction("CreateGroup", new ErrorCreateGroupInputModel
                {
                    ErrotType = ErrorCreateGroupType.SuchGroupPriorityExists,
                    Name = inputModel.Name,
                    Priority = inputModel.Priority,
                    EmojiSequence = inputModel.EmojiSequence
                });
            }

            return RedirectToAction("Group");
        }
    }
}