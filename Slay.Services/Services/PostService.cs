using System.Collections.Generic;
using AutoMapper;
using Slay.DalContracts.Repositories;
using Slay.Models.BusinessObjects.Post;
using Slay.ServicesContracts.Services;
using Slay.Utilities.ServiceResult;
using System.Threading.Tasks;
using Slay.DalContracts.Options;
using Slay.Models.Entities;
using Slay.ServicesContracts.Providers.ValidationsProviders;
using Slay.Utilities.Extensions;

namespace Slay.Services.Services
{
	public sealed class PostService : IPostService
    {
	    private readonly IValidationsProvider _validationsProvider;

		private readonly IPostRepository _postRepository;

	    private readonly IMapper _mapper;

	    public PostService(IValidationsProvider validationsProvider, IPostRepository postRepository, IMapper mapper)
	    {
		    this._validationsProvider = validationsProvider;
		    this._postRepository = postRepository;

		    this._mapper = mapper;
	    }

        public async Task<ServiceResult<PostItemBo>> GetPostByIdAsync(string id)
        {
	        if (string.IsNullOrEmpty(id))
	        {
				return new ServiceResult<PostItemBo> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
			}

	        var repositoryResult = await this._postRepository.GetByIdAsync(id);

	        var mapperResult = this._mapper.Map<PostItemBo>(repositoryResult);

			return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

	    public async Task<ServiceResult<PostsResponseBo>> GetPostsAsync(int skip, int limit)
		{
			var pagingOptions = new PagingOptions().SkipItems(skip).LimitItems(limit);
			var sortingOptions = new SortingOptions("CreatedOn");

			var sortOptions = new List<SortingOptions> { sortingOptions };

			var repositoryResult = await this._postRepository.GetAsync(post => post.IsDeleted == false, pagingOptions, sortOptions);

			var mapperResult = this._mapper.Map<IEnumerable<PostItemBo>>(repositoryResult);

			var postsResponseBo = await this.MapPostsResultsWithPageOptions(skip, limit, mapperResult);

			return new ServiceResult<PostsResponseBo> { Value = postsResponseBo };
		}

		public async Task<ServiceResult<PostItemBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = await this._validationsProvider.CreatePostValidator.ValidateAsync(createPostRequestBo);

	        if (!validationResult.IsValid)
	        {
		        return new ServiceResult<PostItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
	        }

	        var repositoryResult = await this._postRepository.CreateAsync(this._mapper.Map<PostEntity>(createPostRequestBo));

	        var mapperResult = this._mapper.Map<PostItemBo>(repositoryResult);

			return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

	    public async Task<ServiceResult<bool>> DeletePostAsync(string id)
	    {
			if (string.IsNullOrEmpty(id))
			{
				return new ServiceResult<bool> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
			}

		    var result = await this._postRepository.DeleteAsync(id);

			return new ServiceResult<bool> { Value = result };
	    }

	    private async Task<PostsResponseBo> MapPostsResultsWithPageOptions(int skip, int limit, IEnumerable<PostItemBo> mapperResult)
	    {
		    var postsCount = await this._postRepository.CountAsync(postEntity => postEntity.IsDeleted == false);

		    var postsResponseBo = new PostsResponseBo
		    {
			    Posts = mapperResult,
			    Skip = skip + limit >= postsCount ? (int?) null : skip + limit,
			    Limit = limit
		    };

		    return postsResponseBo;
	    }
	}
}