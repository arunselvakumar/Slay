#pragma warning disable 1998
namespace Slay.Business.ServicesContracts.Services
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Category;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    [ContractClass(typeof(IPostCategoryServiceContract))]
    public interface IPostCategoryService
    {
        Task<ServiceResult<CreateCategoryResponseBo>> CreateCategoryAsync([NotNull] CreateCategoryRequestBo category, CancellationToken token);

        Task<ServiceResult<CategoriesListResponseBo>> GetCategoriesAsync(CancellationToken token);
    }

    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "https://docs.microsoft.com/en-us/dotnet/framework/debug-trace-profile/code-contracts")]
    [ContractClassFor(typeof(IPostCategoryService))]
    internal abstract class IPostCategoryServiceContract : IPostCategoryService
    {
        public async Task<ServiceResult<CreateCategoryResponseBo>> CreateCategoryAsync(CreateCategoryRequestBo category, CancellationToken token)
        {
            Contract.Requires(category.IsNotNull());
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<CreateCategoryResponseBo>>().IsNotNull());

            return default(ServiceResult<CreateCategoryResponseBo>);
        }

        public async Task<ServiceResult<CategoriesListResponseBo>> GetCategoriesAsync(CancellationToken token)
        {
            Contract.Requires(token.IsNotNull());

            Contract.Ensures(Contract.Result<ServiceResult<CategoriesListResponseBo>>().IsNotNull());

            return default(ServiceResult<CategoriesListResponseBo>);
        }
    }
}