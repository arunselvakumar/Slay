﻿namespace Slay.Business.Services.Facades
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Slay.Business.ServicesContracts.Facades;
    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;

    public sealed class AzureStorageServicesFacade : IAzureStorageServicesFacade
    {
        private static readonly CloudBlobClient AzureBlobClient;

        /// <summary>
        /// Initializes static members of the <see cref="AzureStorageServicesFacade"/> class. 
        /// </summary>
        /// <exception cref="ApplicationException">
        /// Connection String
        /// </exception>
        static AzureStorageServicesFacade()
        {
            string storageConnectionString = @"DefaultEndpointsProtocol=https;AccountName=aruntestz;AccountKey=AQKvq9R31RTDNMc1yjmE7QON3RLCnEHYesOSLlB1Ov4HpJCoshNxkDvSiHc8F3jEUCaEzFBEydv2VFxRLR6SCQ==;EndpointSuffix=core.windows.net";

            if (CloudStorageAccount.TryParse(storageConnectionString, out var storageAccount))
            {
                AzureBlobClient = storageAccount.CreateCloudBlobClient();
            }
            else
            {
                throw new ApplicationException();
            }
        }

        public async Task<CloudBlockBlob> SaveBlobInContainerAsync(TemplateUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            try
            {
                var fileName = uploadRequestContext.File.FileName;
                var containerName = "templates";

                return await this.UploadFileAsync(uploadRequestContext.File, fileName, containerName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CloudBlockBlob> SaveBlobInContainerAsync(PostUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            try
            {
                var fileName = uploadRequestContext.File.FileName;
                var containerName = $"{uploadRequestContext.User.GetUserId()}-{uploadRequestContext.RequestType}";

                return await this.UploadFileAsync(uploadRequestContext.File, fileName, containerName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<CloudBlockBlob> UploadFileAsync(IFormFile file, string fileName, string containerName)
        {
            var blobContainer = AzureBlobClient.GetContainerReference(containerName);

            await blobContainer.CreateIfNotExistsAsync();
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            var blockBlock = blobContainer.GetBlockBlobReference(this.GenerateSlug(fileName));

            using (var fileStream = file.OpenReadStream())
            {
                await blockBlock.UploadFromStreamAsync(fileStream);
            }

            return blockBlock;
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