namespace Slay.Business.Services.Facades
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Slay.Business.ServicesContracts.Facades;
    using Slay.Utilities.ServiceResult;

    public sealed class AzureStorageServicesFacade : IAzureStorageServicesFacade
    {
        private static readonly CloudBlobClient CloudBlobClient;

        static AzureStorageServicesFacade()
        {
            string storageConnectionString =
                @"DefaultEndpointsProtocol=https;AccountName=aruntestz;AccountKey=AQKvq9R31RTDNMc1yjmE7QON3RLCnEHYesOSLlB1Ov4HpJCoshNxkDvSiHc8F3jEUCaEzFBEydv2VFxRLR6SCQ==;EndpointSuffix=core.windows.net";

            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                CloudBlobClient = storageAccount.CreateCloudBlobClient();
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public async Task<ServiceResult<string>> SaveBlobInContainerAsync(string containerName, IFormFile file, CancellationToken token)
        {
            try
            {
                var fileName = file.FileName;

                var blobContainer = CloudBlobClient.GetContainerReference(containerName + "-container");

                await blobContainer.CreateIfNotExistsAsync();
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                var blockBlock = blobContainer.GetBlockBlobReference(this.SlugFileName(fileName));

                using (var fileStream = file.OpenReadStream())
                {
                    await blockBlock.UploadFromStreamAsync(fileStream);
                }

                return new ServiceResult<string> { Value = blockBlock.StorageUri.PrimaryUri.AbsoluteUri };
            }
            catch (Exception)
            {
                return new ServiceResult<string>();
            }
        }

        private string SlugFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            var randomFileName = Path.GetRandomFileName();
            var randomFileNameWithoutExtension = Path.GetFileNameWithoutExtension(randomFileName);

            var timeStamp = DateTime.UtcNow.ToFileTimeUtc().ToString();

            var uniqueFileName = $"{randomFileNameWithoutExtension}_{fileNameWithoutExtension}_{timeStamp}{extension}";
            return uniqueFileName;
        }
    }
}