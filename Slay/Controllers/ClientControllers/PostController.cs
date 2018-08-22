namespace Slay.Host.Controllers.ClientControllers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Models.DataTransferObjects.Category;
    using Slay.Models.DataTransferObjects.Post.Links;
    using Slay.Models.DataTransferObjects.Post.Request;
    using Slay.Models.DataTransferObjects.Post.Response;

    /// <summary>
    /// The post controller.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Post")]
    public sealed class PostController : ApiBaseController
    {
        private readonly IMapper _autoMapperService;

        private readonly IPostService _postService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostController"/> class.
        /// </summary>
        /// <param name="autoMapperService">The automatic mapper service.</param>
        /// <param name="postService">The post service.</param>
        public PostController(IMapper autoMapperService, IPostService postService)
        {
            this._autoMapperService = autoMapperService;
            this._postService = postService;
        }

        /// <summary>
        /// Gets a post by its ID.
        /// </summary>
        /// <param name="id">Post ID.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// If post is found, then a <see cref="PostResponseDto"/> is returned.
        /// If no post is found, then an <see cref="NotFoundResult"/> is returned
        /// </returns>
        [HttpGet("{id}", Name = nameof(GetPostByIdAsync))]
        [ProducesResponseType(200, Type = typeof(PostResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPostByIdAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._postService.GetPostByIdAsync(id, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                if (serviceResult.Value == null)
                {
                    return new NotFoundResult();
                }

                var mapperResult = this._autoMapperService.Map<PostResponseDto>(serviceResult.Value);

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// Gets the all posts based on paging.
        /// </summary>
        /// <param name="skip">Skip Posts.</param>
        /// <param name="limit">Limits to retrieve.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// <see cref="PostsListResponseDto"/> is returned.
        /// </returns>
        [HttpGet(Name = nameof(GetPostsAsync))]
        [ProducesResponseType(200, Type = typeof(PostsListResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostsAsync([FromQuery] int skip = 0, [FromQuery] int limit = 10, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._postService.GetPostsAsync(skip, limit, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<PostsListResponseDto>(serviceResult.Value);

                mapperResult.Links = new LinksDto
                {
                    Base = this.GetBaseUrl(),
                    Self = Url.Link(nameof(this.GetPostsAsync), new { skip = (int?)skip, limit = (int?)limit }),
                    Next = Url.Link(nameof(this.GetPostsAsync), new { skip = serviceResult.Value.Skip, limit = serviceResult.Value.Limit })
                };

                mapperResult.Data.ToList().ForEach(post => post.Links = new LinksDto { Base = this.GetBaseUrl(), Self = Url.Link(nameof(this.GetPostsAsync), new { id = post.Data.Id }) });

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }

        /// <summary>
        /// Creates a Post.
        /// </summary>
        /// <param name="createPostDto">The create post dto.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// If post is created, then a 201 response code with created at route is returned with <see cref="PostDto"/> .
        /// Else a 400 response is returned.
        /// </returns>
        [HttpPost(Name = nameof(CreatePostAsync))]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(PostDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequestDto createPostDto, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var createPostBo = this._autoMapperService.Map<CreatePostRequestBo>(createPostDto);

                var serviceResult = await this._postService.CreatePostAsync(createPostBo, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mappedResult = this._autoMapperService.Map<PostResponseDto>(serviceResult.Value);

                return this.CreatedAtRoute(nameof(this.CreatePostAsync), new { id = mappedResult.Data.Id }, mappedResult);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        /// <summary>
        /// Deletes the post based on the ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// If post is deleted, then a 200 response code is returned.
        /// If post is not deleted, then a 400 response is returned.
        /// </returns>
        [HttpDelete("{id}", Name = nameof(DeletePostAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeletePostAsync(string id, CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._postService.DeletePostAsync(id, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                return new EmptyResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        private string GetBaseUrl()
        {
            return Request.Scheme + "://" + Request.Host + Request.PathBase.Value.TrimEnd('/') + "/";
        }
    }
}