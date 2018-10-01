using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkBot.Manager.Helpers;
using VkNet;
using VkNet.Enums.SafetyEnums;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkBot.Manager.Services.Vk
{
    public interface IVkCompetitionService
    {
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostsAfterDate(DateTime? date);
        IEnumerable<User> GetLikeUsersOnPost(long? postId);
        IEnumerable<User> GetLikeUsers(LikeObjectType type, long? itemId);
    }

    public class VkCompetitionService : IVkCompetitionService
    {
        private readonly IConfigurationHelperService _configurationHelper;
        private readonly VkApi _api;

        public VkCompetitionService(IVkConnection connection, IConfigurationHelperService configurationHelper)
        {
            _configurationHelper = configurationHelper;
            _api = connection.GetVkApi(configurationHelper);
        }

        public IEnumerable<Post> GetPosts()
        {
            ulong offset = 0;

            var postTasks = new List<Task<WallGetObject>>();

            while (true)
            {
                var postTask = _api.Wall.GetAsync(new WallGetParams
                {
                    OwnerId = _configurationHelper.GetGroupId() * -1,
                    Count = 100
                });

                postTasks.Add(postTask);

                offset += 100;

                var postCount = postTask.Result.TotalCount;

                if (offset > postCount)
                {
                    break;
                }
            }

            var resultCollection = new List<Post>();

            foreach (var wallObject in postTasks.Select(p => p.Result))
            {
                resultCollection.AddRange(wallObject.WallPosts);
            }

            return resultCollection;
        }

        public IEnumerable<Post> GetPostsAfterDate(DateTime? date)
        {
            var posts = GetPosts();

            return date == null ? posts : posts.Where(p => p.Date > date);
        }

        public IEnumerable<User> GetLikeUsersOnPost(long? postId)
        {
            return GetLikeUsers(LikeObjectType.Post, postId);
        }

        public IEnumerable<User> GetLikeUsers(LikeObjectType type, long? itemId)
        {
            var likeTask = _api.Likes.GetListExAsync(new LikesGetListParams
            {
                Type = LikeObjectType.Post,
                OwnerId = _configurationHelper.GetGroupId() * -1,
                ItemId = itemId ?? 0
            });

            var likes = likeTask.Result;

            return likes.Users;
        }
    }
}