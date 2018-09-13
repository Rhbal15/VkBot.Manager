using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.ViewModels.AdviceViewModels;

namespace VkBot.Manager.Controllers
{
    public class AdviceController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexAdviceViewModel();

            return View(model);
        }
    }
}