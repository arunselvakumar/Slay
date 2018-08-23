namespace Slay.Business.Services.Facades
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Slay.Business.ServicesContracts.Facades;
    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class AzureStorageServicesFacade : IAzureStorageServicesFacade
    {
        private static readonly CloudBlobClient AzureBlobClient;

        static AzureStorageServicesFacade()
        {
            string storageConnectionString =
                @"DefaultEndpointsProtocol=https;AccountName=aruntestz;AccountKey=AQKvq9R31RTDNMc1yjmE7QON3RLCnEHYesOSLlB1Ov4HpJCoshNxkDvSiHc8F3jEUCaEzFBEydv2VFxRLR6SCQ==;EndpointSuffix=core.windows.net";

            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                AzureBlobClient = storageAccount.CreateCloudBlobClient();
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public async Task<ServiceResult<string>> SaveBlobInContainerAsync(FileUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            try
            {
                var fileName = uploadRequestContext.File.FileName;
                var containerName = $"{uploadRequestContext.User.GetUserId()}-{uploadRequestContext.RequestType}";

                var blobContainer = AzureBlobClient.GetContainerReference(containerName);

                await blobContainer.CreateIfNotExistsAsync();
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                var blockBlock = blobContainer.GetBlockBlobReference(this.GenerateSlug(fileName));

                using (var fileStream = uploadRequestContext.File.OpenReadStream())
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

        private string GenerateSlug(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            var randomFileName = Path.GetRandomFileName();
            var randomFileNameWithoutExtension = Path.GetFileNameWithoutExtension(randomFileName);

            var timeStamp = DateTime.UtcNow.ToFileTimeUtc().ToString();

            var uniqueFileName = $"{fileNameWithoutExtension}_{randomFileNameWithoutExtension}_{timeStamp}{extension}";
            return uniqueFileName;
        }
    }
}