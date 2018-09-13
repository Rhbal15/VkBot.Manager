using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkBot.Manager.Helpers;
using VkNet;
using VkNet.Model;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace VkBot.Manager.Services.Vk
{
    public class VkGroupPhotoService : IVkGroupPhotoService
    {
        private readonly IConfigurationHelperService _configurationHelper;
        private readonly VkApi _api;

        public VkGroupPhotoService(IConfigurationHelperService configurationHelper, IVkConnection vkConnection)
        {
            _configurationHelper = configurationHelper;
            _api = vkConnection.GetVkApi(configurationHelper);
        }

        public Task<PhotoAlbum> CreateAlbum(string name)
        {
            return _api.Photo.CreateAlbumAsync(new PhotoCreateAlbumParams
            {
                Title = name,
                GroupId = _configurationHelper.GetGroupId()
            });
        }

        public async Task<Photo> UploadImage(long albumId, string imageUrl)
        {
            var uploadServer = await _api.Photo.GetUploadServerAsync(albumId, _configurationHelper.GetGroupId());
            // Загрузить файл.

            var tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"{System.Guid.NewGuid()}.webp");

            var wc = new WebClient();
            wc.DownloadFile(imageUrl, tempFilePath);
            ;
            var responseFile =
                Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, tempFilePath));

            File.Delete(tempFilePath);

            return _api.Photo.Save(new PhotoSaveParams
            {
                SaveFileResponse = responseFile,
                AlbumId = albumId,
                GroupId = _configurationHelper.GetGroupId()
            }).FirstOrDefault();
        }

        public IEnumerable<Photo> Get(long? photoId)
        {
            return photoId == null
                ? null
                : _api.Photo.GetById(new[] {$"-{_configurationHelper.GetGroupId()}_{photoId}"});
        }

        public string GetPhotoUrl(long? photoId)
        {
            if (photoId == null)
            {
                return "";
            }

            var photos = Get(photoId).ToList();

            if (!photos.Any())
            {
                return "";
            }

            var photo = photos.FirstOrDefault();

            return photo?.Sizes.FirstOrDefault(p => p.Height > 200)?.Url.AbsoluteUri;
        }

        public void RemovePhoto(long? vkPhotoId)
        {
            if (!vkPhotoId.HasValue)
            {
                return;
            }

            try
            {
                _api.Photo.Delete((ulong) vkPhotoId.Value, -_configurationHelper.GetGroupId());
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}