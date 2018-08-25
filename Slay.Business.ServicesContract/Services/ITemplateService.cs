namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.ServiceResult;

    public interface ITemplateService
    {
        Task<ServiceResult<bool>> UploadTemplateAsync([NotNull] TemplateUploadRequestContext uploadRequestContext, CancellationToken token);
    }
}