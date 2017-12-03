using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Slay.DalContracts.Options;
using Slay.DalContracts.Repositories;
using Slay.Models.BusinessObjects.Comment;
using Slay.Models.Entities;
using Slay.ServicesContracts.Providers.ValidationsProviders;
using Slay.ServicesContracts.Services;
using Slay.Utilities.Extensions;
using Slay.Utilities.ServiceResult;

namespace Slay.Services.Services
{
	public sealed class CommentService : ICommentService
	{
		private readonly IValidationsProvider _validationsProvider;

		private readonly ICommentRepository _commentRepository;

		private readonly IMapper _mapper;

		public CommentService(IValidationsProvider validationsProvider, ICommentRepository commentRepository, IMapper mapper)
		{
			this._validationsProvider = validationsProvider;
			this._commentRepository = commentRepository;

			this._mapper = mapper;
		}

		public async Task<ServiceResult<CommentItemBo>> CreateCommentAsync(string postId, string commentId, CreateCommentRequestBo createCommentRequestBo)
		{
			var validationResult = await this._validationsProvider.CreateCommentValidator.ValidateAsync(createCommentRequestBo);

			if (!validationResult.IsValid)
			{
				return new ServiceResult<CommentItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
			}

			var repositoryResult = await this._commentRepository.CreateAsync(this._mapper.Map<CommentEntity>(createCommentRequestBo, opt => { opt.Items["PostId"] = postId; opt.Items["ParentId"] = commentId; }));

			var mapperResult = this._mapper.Map<CommentItemBo>(repositoryResult);

			return new ServiceResult<CommentItemBo> { Value = mapperResult };
		}

		public async Task<ServiceResult<CommentsResponseBo>> GetCommentsAsync(string postId, string commentId, int skip, int limit)
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

			var repositoryResult = await this._commentRepository.GetAsync(filterCondition, pagingOptions, sortOptions);

			var mapperResult = this._mapper.Map<IEnumerable<CommentItemBo>>(repositoryResult);

			var commentResponseBo = await this.MapCommentsResultsWithPageOptions(postId, commentId, skip, limit, mapperResult);

			return new ServiceResult<CommentsResponseBo> { Value = commentResponseBo };
		}

		private async Task<CommentsResponseBo> MapCommentsResultsWithPageOptions(string postId, string commentId, int skip, int limit, IEnumerable<CommentItemBo> mapperResult)
		{
			long postsCount;

			postsCount = string.IsNullOrWhiteSpace(commentId)
						? await this._commentRepository.CountAsync(comment => !comment.IsDeleted && comment.PostId == postId)
						: await this._commentRepository.CountAsync(comment => !comment.IsDeleted && comment.PostId == postId && comment.ParentId == commentId);

			var commentsResponseBo = new CommentsResponseBo
			{
				Comments = mapperResult.ForEach(async comment => comment.ChildrensCount = await this._commentRepository.CountAsync(cx => !cx.IsDeleted && cx.ParentId == comment.Id)).ToList(),
				Skip = skip + limit >= postsCount ? (int?)null : skip + limit,
				Limit = limit
			};

			return commentsResponseBo;
		}
	}
}