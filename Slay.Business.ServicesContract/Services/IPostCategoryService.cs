namespace Slay.Business.ServicesContracts.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Slay.Models.BusinessObjects.Category;
    using Slay.Utilities.ServiceResult;

    public interface IPostCategoryService
    {
        Task<ServiceResult<CreateCategoryResponseBo>> CreateCategoryAsync([NotNull] CreateCategoryRequestBo category, CancellationToken token);

        Task<ServiceResult<CategoriesListResponseBo>> GetCategoriesAsync(CancellationToken token);
    }
}