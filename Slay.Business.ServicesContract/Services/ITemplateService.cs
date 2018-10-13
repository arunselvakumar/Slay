#pragma warning disable 1998
namespace Slay.Business.ServicesContracts.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Template;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    [ContractClass(typeof(ITemplateServiceContract))]
    public interface ITemplateService
    {
        Task<ServiceResult<TemplateItemBo>> UploadTemplateAsync([NotNull] TemplateUploadRequestContext uploadRequestContext, CancellationToken token);

        Task<ServiceResult<TemplateListResponseBo>> GetTemplatesAsync(int skip, int limit, CancellationToken token);

        Task<ServiceResult<TemplateItemBo>> GetTemplateByIdAsync([NotNull] string id, CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(ITemplateService))]
    internal abstract class ITemplateServiceContract : ITemplateService
    {
        public async Task<ServiceResult<TemplateItemBo>> UploadTemplateAsync(TemplateUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            Contract.Requires(uploadRequestContext.IsNotNull());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<TemplateItemBo>>().IsNotNull());

            return default(ServiceResult<TemplateItemBo>);
        }

        public async Task<ServiceResult<TemplateListResponseBo>> GetTemplatesAsync(int skip, int limit, CancellationToken token)
        {
            Contract.Requires(skip >= 0);
            Contract.Requires(limit >= 0);
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<TemplateListResponseBo>>().IsNotNull());

            return default(ServiceResult<TemplateListResponseBo>);
        }

        public async Task<ServiceResult<TemplateItemBo>> GetTemplateByIdAsync(string id, CancellationToken token)
        {
            Contract.Requires(id.IsNotNullOrEmpty());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<TemplateItemBo>>().IsNotNull());

            return default(ServiceResult<TemplateItemBo>);
        }
    }
}