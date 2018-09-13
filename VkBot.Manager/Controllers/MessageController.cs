using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.ViewModels.MessageViewModels;

namespace VkBot.Manager.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            var model = new IndexMessageViewModel();

            return View(model);
        }
    }
}