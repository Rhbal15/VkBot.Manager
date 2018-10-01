using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VkBot.Manager.Data;
using VkBot.Manager.Exceptions;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.EmojisViewModels;

namespace VkBot.Manager.Controllers
{
    [Authorize]
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
            var groups = _emojiService.GetAllEmojiGroupsSortedByPriority();

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

        public IActionResult GroupDetail(int id)
        {
            var emojiGroup = _emojiService.GetGroup(id);

            var model = new GroupDetailEmojiViewModel
            {
                Id = emojiGroup.Id,
                Name = emojiGroup.Name,
                CreatedDate = emojiGroup.CreateDate.ToString(CultureInfo.CurrentCulture),
                Priority = emojiGroup.Priority,
                Emojis = emojiGroup.Emojis.Select(p => new EmojiGroupDetailEmojiViewModel
                {
                    Id = p.Id,
                    Symbol = p.Symbol,
                    IsOnKeyboard = p.KeyboardButtons
                        .Any(b => b.Keyboard.KeyboardStatus == KeyboardStatus.Active),
                    Involves = p.EmojiInvolves.Count(),
                    Description = p.EmojiDescriptions
                        .Select(d => d.Text).Join("\r\n")
                }),
                GroupInvolves = emojiGroup.Emojis
                    .Sum(p => p.EmojiInvolves.Count())
            };

            return View(model);
        }

        public IActionResult GroupEdit(int id)
        {
            var model = new GroupEditEmojiViewModel
            {
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult GroupEdit(GroupEditEmojiInputModel inputModel)
        {
            return RedirectToAction("GroupDetail", new {inputModel.Id});
        }

        public IActionResult GroupDelete(int id)
        {
            return RedirectToAction("Group");
        }
    }

    public class EmojiGroupDetailEmojiViewModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public bool IsOnKeyboard { get; set; }
        public int Involves { get; set; }
        public string Description { get; set; }
    }

    public class GroupEditEmojiViewModel
    {
    }

    public class GroupDetailEmojiViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public int Priority { get; set; }
        public IEnumerable<EmojiGroupDetailEmojiViewModel> Emojis { get; set; }
        public int GroupInvolves { get; set; }
    }

    public class GroupEditEmojiInputModel
    {
        public int Id { get; set; }
    }
}