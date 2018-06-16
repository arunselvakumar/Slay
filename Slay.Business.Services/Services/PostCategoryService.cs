namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Options;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.Entities;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class PostCategoryService : IPostCategoryService
    {
        private readonly IMapper _autoMapperService;

        private readonly IValidationsProvider _validationsProvider;

        private readonly ICategoryRepository _categoryRepository;

        public PostCategoryService(IMapper autoMapperService, IValidationsProvider validationsProvider, ICategoryRepository categoryRepository)
        {
            this._autoMapperService = autoMapperService;
            this._validationsProvider = validationsProvider;
            this._categoryRepository = categoryRepository;
        }

        public async Task<ServiceResult<CategoryItemBo>> CreateCategoryAsync(CreateCategoryRequestBo category, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.CreateCategoryValidator.ValidateAsync(category, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<CategoryItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var repositoryResult = await this._categoryRepository.CreateAsync(this._autoMapperService.Map<CategoryEntity>(category), token);

            var mapperResult = this._autoMapperService.Map<CategoryItemBo>(repositoryResult);

            return new ServiceResult<CategoryItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<CategoriesListResponseBo>> GetAllCategoriesAsync(CancellationToken token)
        {
            var sortingOptions = new List<SortingOptions> { new SortingOptions("Order") };

            var repositoryResult = await this._categoryRepository.GetAsync(null, null, sortingOptions, token);

            var mapperResult = this._autoMapperService.Map<IEnumerable<CategoryItemBo>>(repositoryResult);

            return new ServiceResult<CategoriesListResponseBo> { Value = new CategoriesListResponseBo { Categories = mapperResult } };
        }
    }
}