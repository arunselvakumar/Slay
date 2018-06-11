namespace Slay.Host.Controllers.ClientControllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.DataTransferObjects.Comment;
    using Slay.Models.DataTransferObjects.Post.Links;

    [Produces("application/json")]
    [Route("api/Post/{postId}/Comment")]
    public sealed class CommentController : ApiBaseController
    {
        private readonly ICommentService _commentService;

        private readonly IMapper _autoMapperService;

        public CommentController(IMapper autoMapperService, ICommentService commentService)
        {
            this._autoMapperService = autoMapperService;

            this._commentService = commentService;
        }

        [HttpPost("{commentId?}")]
        [ProducesResponseType(201, Type = typeof(CommentResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CreateCommentRequestDto createCommentRequestDto, string postId, [FromRoute]string commentId = null)
        {
            var createCommentBo = this._autoMapperService.Map<CreateCommentRequestBo>(createCommentRequestDto);

            var serviceResult = await this._commentService.CreateCommentAsync(postId, commentId, createCommentBo);

            if (serviceResult.HasErrors)
            {
                return new BadRequestObjectResult(serviceResult.Errors);
            }

            var mappedResult = this._autoMapperService.Map<CommentResponseDto>(serviceResult.Value);

            return this.CreatedAtRoute(nameof(this.GetCommentsAsync), new { postId = postId, commentId = mappedResult.Data.Id }, mappedResult);
        }

        [HttpGet("{commentId?}", Name = nameof(GetCommentsAsync))]
        [ProducesResponseType(201, Type = typeof(CommentsListResponseDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCommentsAsync(string postId, string commentId, [FromQuery] int skip = 0, [FromQuery] int limit = 10)
        {
            try
            {
                var serviceResult = await this._commentService.GetCommentsAsync(postId, commentId, skip, limit);

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