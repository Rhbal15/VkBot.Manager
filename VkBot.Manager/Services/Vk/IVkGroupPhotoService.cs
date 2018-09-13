using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace VkBot.Manager.Services.Vk
{
    public interface IVkGroupPhotoService
    {
        Task<PhotoAlbum> CreateAlbum(string name);
        Task<Photo> UploadImage(long albumId, string imageUrl);
        IEnumerable<Photo> Get(long? photoId);
        string GetPhotoUrl(long? photoId);
        void RemovePhoto(long? vkPhotoId);
    }
}