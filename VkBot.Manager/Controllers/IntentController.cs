using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.ViewModels.IntentViewModels;

namespace VkBot.Manager.Controllers
{
    public class IntentController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexIntentViewModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PostCreateIntentViewModel intentViewModel)
        {


            return RedirectToAction("Index");
        }
    }
}