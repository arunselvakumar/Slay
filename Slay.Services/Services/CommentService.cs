using System.Threading.Tasks;
using AutoMapper;
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

		private readonly IPostRepository _postRepository;

		private readonly IMapper _mapper;

		public CommentService(IValidationsProvider validationsProvider, IPostRepository postRepository, IMapper mapper)
		{
			this._validationsProvider = validationsProvider;
			this._postRepository = postRepository;

			this._mapper = mapper;
		}

		public async Task<ServiceResult<CommentResponseBo>> CreateCommentAsync(string postId, string commentId, CreateCommentRequestBo createCommentRequestBo)
		{
			var validationResult = await this._validationsProvider.CreateCommentValidator.ValidateAsync(createCommentRequestBo);

			if (!validationResult.IsValid)
			{
				return new ServiceResult<CommentResponseBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
			}

			var repositoryResult = await this._postRepository.CreateCommentAsync(postId, commentId, this._mapper.Map<CommentEntity>(createCommentRequestBo));

			var mapperResult = this._mapper.Map<CommentResponseBo>(repositoryResult);

			return new ServiceResult<CommentResponseBo> { Value = mapperResult };
		}
	}
}