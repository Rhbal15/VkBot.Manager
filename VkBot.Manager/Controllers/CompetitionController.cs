using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VkBot.Manager.Services.Vk;

namespace VkBot.Manager.Controllers
{
    [Authorize]
    public class CompetitionController : Controller
    {
        private readonly IVkCompetitionService _vkCompetitionService;

        public CompetitionController(IVkCompetitionService vkCompetitionService)
        {
            _vkCompetitionService = vkCompetitionService;
        }
        /*Groups.IsMember */

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleLikeCountCompetition()
        {
            var posts = _vkCompetitionService.GetPosts();

            var resultMap = new Dictionary<long, int>();

            foreach (var post in posts)
            {
                var likeUsers = _vkCompetitionService.GetLikeUsersOnPost(post.Id);

                foreach (var likeUser in likeUsers)
                {
                    if (resultMap.ContainsKey(likeUser.Id))
                    {
                        resultMap[likeUser.Id]++;
                    }
                    else
                    {
                        resultMap.Add(likeUser.Id, 1);
                    }
                }
            }

            return View();
        }
    }
}