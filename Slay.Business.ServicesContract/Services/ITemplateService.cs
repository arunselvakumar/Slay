namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Template;
    using Slay.Utilities.ServiceResult;

    public interface ITemplateService
    {
        Task<ServiceResult<TemplateItemBo>> UploadTemplateAsync([NotNull] TemplateUploadRequestContext uploadRequestContext, CancellationToken token);

        Task<ServiceResult<TemplateListResponseBo>> GetTemplatesAsync(int skip, int limit, CancellationToken token);

        Task<ServiceResult<TemplateItemBo>> GetTemplateByIdAsync([NotNull] string id, CancellationToken token);
    }
}