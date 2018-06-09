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
        public async Task<IActionResult> CreateCommentAsync(
            string postId,
            string commentId,
            [FromBody] CreateCommentRequestDto createCommentRequestDto)
        {
            var createCommentBo = this._autoMapperService.Map<CreateCommentRequestBo>(createCommentRequestDto);

            var serviceResult = await this._commentService.CreateCommentAsync(postId, commentId, createCommentBo);

            if (serviceResult.HasErrors)
            {
                return new BadRequestObjectResult(serviceResult.Errors);
            }

            return new OkObjectResult(this._autoMapperService.Map<CommentItemDto>(serviceResult.Value));
        }

        [HttpGet("{commentId?}", Name = nameof(GetCommentsAsync))]
        public async Task<IActionResult> GetCommentsAsync(
            string postId,
            string commentId,
            [FromQuery] int skip = 0,
            [FromQuery] int limit = 10)
        {
            try
            {
                var serviceResult = await this._commentService.GetCommentsAsync(postId, commentId, skip, limit);

                if (serviceResult.HasErrors)
                {
                    return new BadRequestObjectResult(serviceResult.Errors);
                }

                var mapperResult = this._autoMapperService.Map<CommentsListResponseDto>(serviceResult.Value);

                mapperResult.Links = new LinksDto
                                         {
                                             Base = this.GetBaseUrl(),
                                             Self = Url.Link(
                                                 nameof(GetCommentsAsync),
                                                 new
                                                     {
                                                         postId = postId,
                                                         commentId = commentId,
                                                         skip = (int?)skip,
                                                         limit = (int?)limit
                                                     }),
                                             Next = Url.Link(
                                                 nameof(GetCommentsAsync),
                                                 new
                                                     {
                                                         postId = postId,
                                                         commentId = commentId,
                                                         skip = serviceResult.Value.Skip,
                                                         limit = serviceResult.Value.Limit
                                                     })
                                         };

                mapperResult.Data.ToList().ForEach(
                    comment => comment.Links = new LinksDto
                                                   {
                                                       Base = this.GetBaseUrl(),
                                                       Descendants =
                                                           comment.Data.Descendants > 0
                                                               ? Url.Link(
                                                                   nameof(GetCommentsAsync),
                                                                   new
                                                                       {
                                                                           postId = comment.Data.PostId,
                                                                           commentId = comment.Data.Id,
                                                                           skip = (int?)skip,
                                                                           limit = (int?)limit
                                                                       })
                                                               : null
                                                   });

                return new OkObjectResult(mapperResult);
            }
            catch (Exception e)
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