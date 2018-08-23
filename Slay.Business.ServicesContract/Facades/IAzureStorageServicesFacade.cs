namespace Slay.Business.ServicesContracts.Facades
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.ServiceResult;

    public interface IAzureStorageServicesFacade
    {
        Task<ServiceResult<string>> SaveBlobInContainerAsync([NotNull]FileUploadRequestContext uploadRequestContext, CancellationToken token);
    }
}