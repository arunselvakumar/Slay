namespace Slay.Business.ServicesContracts.Facades
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Microsoft.AspNetCore.Http;

    using Slay.Utilities.ServiceResult;

    public interface IAzureStorageServicesFacade
    {
        Task<ServiceResult<string>> SaveBlobInContainerAsync([NotNull]string containerName, [NotNull]IFormFile file, CancellationToken token);
    }
}