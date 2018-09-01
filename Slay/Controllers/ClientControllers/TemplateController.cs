namespace Slay.Host.Controllers.ClientControllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.DataTransferObjects.Shared;
    using Slay.Models.DataTransferObjects.Template;

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
        /// Gets post templates based on paging.
        /// </summary>
        /// <param name="skip">Skip Posts.</param>
        /// <param name="limit">Limits to retrieve.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// <see cref="TemplateListResponseDto"/> is returned.
        /// </returns>
        [HttpGet(Name = nameof(GetTemplatesAsync))]
        [ProducesResponseType(200, Type = typeof(TemplateListResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTemplatesAsync([FromQuery] int skip = 0, [FromQuery] int limit = 10, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._templateService.GetTemplatesAsync(skip, limit, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<TemplateListResponseDto>(serviceResult.Value);

                mapperResult.Links = new LinksDto
                {
                    Base = this.GetBaseUrl(),
                    Self = Url.Link(nameof(this.GetTemplatesAsync), new { skip = (int?)skip, limit = (int?)limit }),
                    Next = Url.Link(nameof(this.GetTemplatesAsync), new { skip = serviceResult.Value.Skip, limit = serviceResult.Value.Limit })
                };

                mapperResult.Data.ToList().ForEach(template => template.Links = new LinksDto { Base = this.GetBaseUrl(), Self = Url.Link(nameof(this.GetTemplateByIdAsync), new { id = template.Data.Id }) });

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// Gets the template by its ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// If template is found, then a <see cref="TemplateResponseDto"/> is returned.
        /// If no template is found, then an <see cref="NotFoundResult"/> is returned
        /// </returns>
        [HttpGet("{id}", Name = nameof(GetTemplateByIdAsync))]
        [ProducesResponseType(200, Type = typeof(TemplateResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTemplateByIdAsync(string id, CancellationToken token)
        {
            try
            {
                var serviceResult = await this._templateService.GetTemplateByIdAsync(id, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                if (serviceResult.Value == null)
                {
                    return new NotFoundResult();
                }

                var mapperResult = this._autoMapperService.Map<TemplateResponseDto>(serviceResult.Value);

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
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
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadTemplateAsync(IFormCollection formCollection, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var formFile = formCollection.Files.First();
                var templateRequestContext = new TemplateUploadRequestContext { File = formFile, User = this.User };
                var serviceResult = await this._templateService.UploadTemplateAsync(templateRequestContext, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<TemplateResponseDto>(serviceResult.Value);

                return this.CreatedAtRoute(nameof(this.GetTemplateByIdAsync), new { id = mapperResult.Data.Id }, mapperResult);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}