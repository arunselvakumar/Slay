namespace Slay.Business.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Options;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.Entities;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        private readonly IMapper _mapper;

        private readonly IValidationsProvider _validationsProvider;

        public CommentService(
            IValidationsProvider validationsProvider,
            ICommentRepository commentRepository,
            IMapper mapper)
        {
            this._validationsProvider = validationsProvider;
            this._commentRepository = commentRepository;

            this._mapper = mapper;
        }

        public async Task<ServiceResult<CommentItemBo>> CreateCommentAsync(string postId, string commentId, CreateCommentRequestBo createCommentRequestBo, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.CreateCommentValidator.ValidateAsync(createCommentRequestBo, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<CommentItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var repositoryResult = await this._commentRepository.CreateAsync(this._mapper.Map<CommentEntity>(createCommentRequestBo, opt => { opt.Items["PostId"] = postId; opt.Items["ParentId"] = commentId; }), token);

            var mapperResult = this._mapper.Map<CommentItemBo>(repositoryResult);

            return new ServiceResult<CommentItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<CommentsListResponseBo>> GetCommentsAsync(string postId, string commentId, int skip, int limit, CancellationToken token)
        {
            var pagingOptions = new PagingOptions().SkipItems(skip).LimitItems(limit);
            var sortingOptions = new SortingOptions("CreatedOn");

            var sortOptions = new List<SortingOptions> { sortingOptions };

            Expression<Func<CommentEntity, bool>> filterCondition;

            if (!string.IsNullOrEmpty(commentId))
            {
                filterCondition = comment => comment.IsDeleted == false && comment.PostId == postId && comment.ParentId == commentId;
            }
            else
            {
                filterCondition = comment => comment.IsDeleted == false && comment.PostId == postId && comment.ParentId == null;
            }

            var repositoryResult = await this._commentRepository.GetAsync(filterCondition, pagingOptions, sortOptions, token);

            var mapperResult = this._mapper.Map<IEnumerable<CommentItemBo>>(repositoryResult);

            var commentResponseBo = await this.MapCommentsResultsWithPageOptions(postId, commentId,  skip, limit, mapperResult, token);

            return new ServiceResult<CommentsListResponseBo> { Value = commentResponseBo };
        }

        private async Task<CommentsListResponseBo> MapCommentsResultsWithPageOptions(string postId, string commentId, int skip, int limit, IEnumerable<CommentItemBo> mapperResult, CancellationToken token)
        {
            long postsCount;

            postsCount = string.IsNullOrWhiteSpace(commentId) ? await this._commentRepository.CountAsync(comment => !comment.IsDeleted && comment.PostId == postId, token)
                                                              : await this._commentRepository.CountAsync(comment => !comment.IsDeleted && comment.PostId == postId && comment.ParentId == commentId, token);

            var commentsResponseBo = new CommentsListResponseBo
            {
                Comments = mapperResult.ForEach(async comment => comment.Descendants = await this._commentRepository.CountAsync(cx => !cx.IsDeleted && cx.ParentId == comment.Id, token)).ToList(),
                Skip = skip + limit >= postsCount ? (int?)null : skip + limit,
                Limit = limit
            };

            return commentsResponseBo;
        }
    }
}