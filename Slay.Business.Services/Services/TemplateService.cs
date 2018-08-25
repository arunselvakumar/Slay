namespace Slay.Business.Services.Services
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Facades;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.File;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class TemplateService : ITemplateService
    {
        private readonly IMapper _autoMapperService;

        private readonly IValidationsProvider _validationsProvider;

        private readonly IAzureStorageServicesFacade _azureStorageServicesFacade;

        public TemplateService(
            IMapper autoMapperService,
            IValidationsProvider validationsProvider,
            IAzureStorageServicesFacade azureStorageServicesFacade)
        {
            this._autoMapperService = autoMapperService;

            this._validationsProvider = validationsProvider;

            this._azureStorageServicesFacade = azureStorageServicesFacade;
        }

        public async Task<ServiceResult<bool>> UploadTemplateAsync(TemplateUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.TemplateUploadValidator.ValidateAsync(uploadRequestContext, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<bool> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var azureStorageResult = await this._azureStorageServicesFacade.SaveBlobInContainerAsync(uploadRequestContext, token);

            if (azureStorageResult.IsNull())
            {
                return new ServiceResult<bool> { Errors = new[] { new Error { Code = "TEMPALTE_FILE_UPLOADFAILED_ERROR" } } };
            }

            return new ServiceResult<bool> { Value = true };
        }
    }
}