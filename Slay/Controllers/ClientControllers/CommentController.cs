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
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.DataTransferObjects.Comment;
    using Slay.Models.DataTransferObjects.Post.Links;

    [Produces("application/json")]
    [Route("api/Post/{postId}/Comment")]
    public sealed class CommentController : ApiBaseController
    {
        private readonly IMapper _autoMapperService;

        private readonly ICommentService _commentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentController"/> class.
        /// </summary>
        /// <param name="autoMapperService">The automatic mapper service.</param>
        /// <param name="commentService">The comment service.</param>
        public CommentController(IMapper autoMapperService, ICommentService commentService)
        {
            this._autoMapperService = autoMapperService;

            this._commentService = commentService;
        }

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="createCommentRequestDto">The create comment request dto.</param>
        /// <param name="postId">The post identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// If comment is created, then a 201 response code with created at route is returned with <see cref="CommentResponseDto"/> .
        /// Else a 400 response is returned.
        /// </returns>
        [HttpPost("{commentId?}")]
        [Authorize]
        [ProducesResponseType(201, Type = typeof(CommentResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCommentAsync(
            [FromBody] CreateCommentRequestDto createCommentRequestDto,
            string postId,
            [FromRoute] string commentId = null,
            CancellationToken token = default(CancellationToken))
        {
            var createCommentBo = this._autoMapperService.Map<CreateCommentRequestBo>(createCommentRequestDto);

            var serviceResult = await this._commentService.CreateCommentAsync(postId, commentId, createCommentBo, token);

            if (serviceResult.HasErrors)
            {
                return new BadRequestObjectResult(serviceResult.Errors);
            }

            var mappedResult = this._autoMapperService.Map<CommentResponseDto>(serviceResult.Value);

            return this.CreatedAtRoute(nameof(this.GetCommentsAsync), new { postId = postId, commentId = mappedResult.Data.Id }, mappedResult);
        }

        /// <summary>
        /// Gets the comments asynchronous.
        /// </summary>
        /// <param name="postId">The post identifier.</param>
        /// <param name="commentId">The comment identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="token">Cancellation Token.</param>
        /// <returns>
        /// <see cref="CommentsListResponseDto"/> is returned.
        /// </returns>
        [HttpGet("{commentId?}", Name = nameof(GetCommentsAsync))]
        [ProducesResponseType(201, Type = typeof(CommentsListResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCommentsAsync(
            string postId,
            string commentId,
            [FromQuery] int skip = 0,
            [FromQuery] int limit = 10,
            CancellationToken token = default(CancellationToken))
        {
            try
            {
                var serviceResult = await this._commentService.GetCommentsAsync(postId, commentId, skip, limit, token);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<CommentsListResponseDto>(serviceResult.Value);

                mapperResult.Data.ToList().ForEach(comment => comment.Links = new LinksDto
                {
                    Base = this.GetBaseUrl(),
                    Descendants = comment.Data.Descendants > 0 ? Url.Link(nameof(this.GetCommentsAsync), new { postId = comment.Data.PostId, commentId = comment.Data.Id, skip = (int?)skip, limit = (int?)limit }) : null
                });

                return new OkObjectResult(mapperResult);
            }
            catch (Exception)
            {
                return new EmptyResult();
            }
        }

        private string GetBaseUrl()
        {
            return Request.Scheme + "://" + Request.Host + Request.PathBase.Value.TrimEnd('/') + "/";
        }
    }
}