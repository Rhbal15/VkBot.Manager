using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace VkBot.Manager.Services
{
    public class AzureImageService : IAzureImageService
    {
        private string AzureStorageConnectionString { get; }
        public const string ImageContainerName = "images";

        public AzureImageService(IConfiguration configuration)
        {
            AzureStorageConnectionString = configuration["AzureStorageConnectionString"];
        }

        public async Task<Uri> UploadImage(string imageName, Stream imageStream)
        {
            var container = GetBlobContainer();
            var blockBlob = container.GetBlockBlobReference(imageName);
            await blockBlob.UploadFromStreamAsync(imageStream);

            return blockBlob.Uri;
        }

        public CloudBlobContainer GetBlobContainer()
        {
            var storageAccount = CloudStorageAccount.Parse(AzureStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(ImageContainerName);
        }
    }
}