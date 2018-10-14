namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using FluentValidation;

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

        private readonly IPostCategoryRepository _postCategoryRepository;

        private readonly IValidator<CreateCategoryRequestBo> _createCategoryValidator;

        public PostCategoryService(
            IMapper autoMapperService,
            IValidator<CreateCategoryRequestBo> createCategoryValidator,
            IPostCategoryRepository postCategoryRepository)
        {
            this._autoMapperService = autoMapperService;
            this._createCategoryValidator = createCategoryValidator;
            this._postCategoryRepository = postCategoryRepository;
        }

        public async Task<ServiceResult<CreateCategoryResponseBo>> CreateCategoryAsync(
            CreateCategoryRequestBo category,
            CancellationToken token)
        {
            var validationResult = await this._createCategoryValidator.ValidateAsync(category, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<CreateCategoryResponseBo>
                {
                    Errors = validationResult.Errors.ToServiceResultErrors()
                };
            }

            var repositoryResult = await this._postCategoryRepository.CreateAsync(this._autoMapperService.Map<PostCategoryEntity>(category), token);

            var mapperResult = this._autoMapperService.Map<CategoryItemBo>(repositoryResult);

            return new ServiceResult<CreateCategoryResponseBo>
            {
                Value = new CreateCategoryResponseBo { Data = mapperResult }
            };
        }

        public async Task<ServiceResult<CategoriesListResponseBo>> GetCategoriesAsync(CancellationToken token)
        {
            var sortingOptions = new List<SortingOptions> { new SortingOptions("Order") };

            var repositoryResult = await this._postCategoryRepository.GetAsync(null, null, sortingOptions, token);

            var mapperResult = this._autoMapperService.Map<IEnumerable<CategoryItemBo>>(repositoryResult.Where(x => x.IsEnabled));

            return new ServiceResult<CategoriesListResponseBo>
            {
                Value = new CategoriesListResponseBo { Categories = mapperResult }
            };
        }
    }
}