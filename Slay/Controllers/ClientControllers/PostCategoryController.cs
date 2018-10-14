namespace Slay.Host.Controllers.ClientControllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.DataTransferObjects.Category;

    [Produces("application/json")]
    [Route("api/Post/Category")]
    public sealed class PostCategoryController : ApiBaseController
    {
        private readonly IMapper _autoMapperService;

        private readonly IPostCategoryService _postCategoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCategoryController"/> class.
        /// </summary>
        /// <param name="autoMapperService">The automatic mapper service.</param>
        /// <param name="postCategoryService">The post category service.</param>
        public PostCategoryController(IMapper autoMapperService, IPostCategoryService postCategoryService)
        {
            this._autoMapperService = autoMapperService;
            this._postCategoryService = postCategoryService;
        }

        /// <summary>
        /// Gets the categories asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// If categories are found, then a <see cref="PostCategoriesListResponseDto"/> is returned.
        /// Else an <see cref="EmptyResult"/> is returned
        /// </returns>
        [HttpGet(Name = nameof(GetPostCategoriesAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostCategoriesAsync(CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._postCategoryService.GetCategoriesAsync(token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<PostCategoriesListResponseDto>(serviceResult.Value);

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// Creates the category asynchronous.
        /// </summary>
        /// <param name="postCategory">The postCategory.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// If post is created, then a 201 response code with created at route is returned with <see cref="CreatePostCategoryResponseDto"/>.
        /// Else a 400 response is returned.
        /// </returns>
        [HttpPost(Name = nameof(CreateCategoryAsync))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody]CreatePostCategoryRequestDto postCategory, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var categoryRequestBo = this._autoMapperService.Map<CreateCategoryRequestBo>(postCategory);

                var serviceResult = await this._postCategoryService.CreateCategoryAsync(categoryRequestBo, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mappedResult = this._autoMapperService.Map<CreatePostCategoryResponseDto>(serviceResult.Value);

                return this.CreatedAtRoute(nameof(this.CreateCategoryAsync), new { id = mappedResult.Data.Id }, mappedResult);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}