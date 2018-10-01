using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.ViewModels.AdviceViewModels;

namespace VkBot.Manager.Controllers
{
    [Authorize]
    public class AdviceController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexAdviceViewModel();

            return View(model);
        }
    }
}