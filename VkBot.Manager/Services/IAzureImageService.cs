using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace VkBot.Manager.Services
{
    public interface IAzureImageService
    {
        Task<Uri> UploadImage(string imageName, Stream imageStream);
        CloudBlobContainer GetBlobContainer();
    }
}