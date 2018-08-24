namespace Slay.Business.ServicesContracts.Facades
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Microsoft.WindowsAzure.Storage.Blob;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.ServiceResult;

    public interface IAzureStorageServicesFacade
    {
        Task<CloudBlockBlob> SaveBlobInContainerAsync([NotNull]PostUploadRequestContext uploadRequestContext, CancellationToken token);
    }
}