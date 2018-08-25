namespace Slay.Host.Controllers.ClientControllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.File;

    [Produces("application/json")]
    [Route("api/Post/Template")]
    public sealed class TemplateController : ApiBaseController
    {
        private readonly IMapper _autoMapperService;

        private readonly ITemplateService _templateService;

        public TemplateController(IMapper autoMapperService, ITemplateService templateService)
        {
            this._autoMapperService = autoMapperService;
            this._templateService = templateService;
        }
        
        /// <summary>
        /// Uploads File to User's Blob Storage Account.
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// If file is uploaded, then a 201 response code is returned.
        /// If file is not uploaded, then a 400 response is returned.
        /// </returns>
        [HttpPost("Upload", Name = nameof(UploadTemplateAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadTemplateAsync(IFormCollection formCollection, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var formFile = formCollection.Files.First();
                var templateRequestContext = new TemplateUploadRequestContext { File = formFile };
                var serviceResult = await this._templateService.UploadTemplateAsync(templateRequestContext, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                return new StatusCodeResult(201);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}