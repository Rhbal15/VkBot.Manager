using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using VkBot.Manager.Exceptions;
using VkBot.Manager.Services.Models;
using VkBot.Manager.ViewModels.IntentViewModels;

namespace VkBot.Manager.Controllers
{
    public class IntentController : Controller
    {
        private readonly IIntentService _intentService;

        public IntentController(IIntentService intentService)
        {
            _intentService = intentService;
        }

        public IActionResult Index()
        {
            var intents = _intentService.GetAllSortedByCreateDate();

            var model = new IndexIntentViewModel
            {
                Intents = intents.Select(p => new IndexIntentListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreatedDate = p.CreateDate.ToString(CultureInfo.CurrentCulture),
                    SentenceCount = p.IntentSentences.Count()
                })
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(ErrorCreateIntentInputModel errorInputModel)
        {
            return View(errorInputModel);
        }

        [HttpPost]
        public IActionResult Create(PostCreateIntentViewModel intentViewModel)
        {
            try
            {
                _intentService.Create(new IntentInputModel
                {
                    Name = intentViewModel.Name,
                    Sentences = intentViewModel.Sentences
                        ?.Split("\r\n")
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .Distinct()
                        .Select(p => p.ToLower().Trim())
                        .Select(p =>
                            new IntentSentenceInputModel
                            {
                                Text = p
                            })
                });
            }
            catch (SuchIntentNameAlreadyExists)
            {
                return RedirectToAction("Create", new ErrorCreateIntentInputModel
                {
                    ErrorType = ErrorCreateIntentType.SuchIntentNameExists,
                    Name = intentViewModel.Name,
                    Sentences = intentViewModel.Sentences
                });
            }

            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var intent = _intentService.Get(id);

            var model = new IntentDetailViewModel
            {
                Id = intent.Id,
                Name = intent.Name,
                CreatedDate = intent.CreateDate,
                Sentences = intent.IntentSentences.Select(p => new SentenceIntentDetailViewModel
                {
                    Id = p.Id,
                    Text = p.Text
                })
            };

            return View(model);
        }

        public IActionResult Edit(int id, ErrorCreateIntentType errorType = ErrorCreateIntentType.None)
        {
            var intent = _intentService.Get(id);

            var model = new EditIntentViewModel
            {
                Id = intent.Id,
                Name = intent.Name,
                CreatedDate = intent.CreateDate.ToString(CultureInfo.CurrentCulture),
                Sentences = intent.IntentSentences.Select(p => p.Text).Join("\r\n"),
                ErrorType = errorType
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditIntentInputModel inputModel)
        {
            try
            {
                _intentService.Edit(new IntentInputModel
                {
                    Id = inputModel.Id,
                    Name = inputModel.Name,
                    Sentences = inputModel.Sentences
                        ?.Split("\r\n")
                        .Where(p => !string.IsNullOrWhiteSpace(p))
                        .Distinct()
                        .Select(p => p.ToLower().Trim())
                        .Select(p =>
                            new IntentSentenceInputModel
                            {
                                Text = p
                            })
                });
            }
            catch (SuchIntentNameAlreadyExists)
            {
                return RedirectToAction("Edit",
                    new {inputModel.Id, ErrorType = ErrorCreateIntentType.SuchIntentNameExists});
            }

            return RedirectToAction("Detail", new {inputModel.Id});
        }

        public IActionResult Delete(int id)
        {
            _intentService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddSentence(AddSentenceIntentInputModel inputModel)
        {
            try
            {
                var intentSentence = _intentService.AddSentence(inputModel.IntentId, new IntentSentenceInputModel
                {
                    Text = inputModel.SentenceText.ToLower().Trim()
                });

                return Ok(new AddSentenceIntentViewModel
                {
                    Id = intentSentence.Id,
                    Text = intentSentence.Text
                });
            }
            catch (SuchIntentSentenceAlreadyExists)
            {
                return BadRequest("Такое предложение уже существует в данном интенте.");
            }
        }

        [HttpDelete]
        public void DeleteSentence(int id)
        {
            _intentService.DeleteSentence(id);
        }
    }
}