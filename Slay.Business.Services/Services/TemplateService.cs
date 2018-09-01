namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Facades;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Options;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Template;
    using Slay.Models.Entities;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class TemplateService : ITemplateService
    {
        private readonly IMapper _autoMapperService;

        private readonly IValidationsProvider _validationsProvider;

        private readonly IAzureStorageServicesFacade _azureStorageServicesFacade;

        private readonly ITemplateRepository _templateRepository;

        public TemplateService(
            IMapper autoMapperService,
            IValidationsProvider validationsProvider,
            IAzureStorageServicesFacade azureStorageServicesFacade,
            ITemplateRepository templateRepository)
        {
            this._autoMapperService = autoMapperService;

            this._validationsProvider = validationsProvider;

            this._azureStorageServicesFacade = azureStorageServicesFacade;

            this._templateRepository = templateRepository;
        }

        public async Task<ServiceResult<TemplateItemBo>> UploadTemplateAsync(TemplateUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.TemplateUploadValidator.ValidateAsync(uploadRequestContext, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<TemplateItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var azureStorageResult = await this._azureStorageServicesFacade.SaveBlobInContainerAsync(uploadRequestContext, token);

            if (azureStorageResult.IsNull())
            {
                return new ServiceResult<TemplateItemBo> { Errors = new[] { new Error { Code = "TEMPALTE_FILE_UPLOADFAILED_ERROR" } } };
            }

            var mapperResult = this._autoMapperService.Map<TemplateEntity>(azureStorageResult);
            mapperResult.CreatedBy = uploadRequestContext.User.GetUserId();

            var repositoryResult = await this._templateRepository.CreateAsync(mapperResult, token);

            return new ServiceResult<TemplateItemBo> { Value = this._autoMapperService.Map<TemplateItemBo>(repositoryResult) };
        }

        public async Task<ServiceResult<TemplateListResponseBo>> GetTemplatesAsync(int skip, int limit, CancellationToken token)
        {
            var pagingOptions = new PagingOptions().SkipItems(skip).LimitItems(limit);
            var sortingOptions = new SortingOptions("CreatedOn");

            var sortOptions = new List<SortingOptions> { sortingOptions };

            var repositoryResult = await this._templateRepository.GetAsync(template => !template.IsDeleted, pagingOptions, sortOptions, token);

            var mapperResult = this._autoMapperService.Map<IEnumerable<TemplateItemBo>>(repositoryResult);

            var templatesResponseBo = await this.MapTemplatesResultsWithPageOptions(skip, limit, mapperResult, token);

            return new ServiceResult<TemplateListResponseBo> { Value = templatesResponseBo };
        }

        public async Task<ServiceResult<TemplateItemBo>> GetTemplateByIdAsync(string id, CancellationToken token)
        {
            if (id.IsNullOrEmpty())
            {
                return new ServiceResult<TemplateItemBo> { Errors = new[] { new Error { Code = "TEMPLATE_TEMPLATEID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._templateRepository.GetByIdAsync(id, token);

            var mapperResult = this._autoMapperService.Map<TemplateItemBo>(repositoryResult);

            return new ServiceResult<TemplateItemBo> { Value = mapperResult };
        }

        private async Task<TemplateListResponseBo> MapTemplatesResultsWithPageOptions(int skip, int limit, IEnumerable<TemplateItemBo> mapperResult, CancellationToken token)
        {
            var count = await this._templateRepository.CountAsync(templateEntity => !templateEntity.IsDeleted, token);

            var templatesResponseBo = new TemplateListResponseBo
            {
                Templates = mapperResult,
                Skip = skip + limit >= count ? (int?)null : skip + limit,
                Limit = limit
            };

            return templatesResponseBo;
        }
    }
}