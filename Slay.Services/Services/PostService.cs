namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Slay.Business.ServicesContracts.Aggregators;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Options;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Models.Entities;
    using Slay.Utilities.Extensions;
    using Slay.Utilities.ServiceResult;

    public sealed class PostService : IPostService
    {
        private readonly IMapper _autoMapperService;

        private readonly IPostRepository _postRepository;

        private readonly IValidationsProvider _validationsProvider;

        private readonly ICommentAggregationService _commentAggregationService;

        public PostService(
            IMapper autoMapperService, 
            IValidationsProvider validationsProvider,
            ICommentAggregationService commentAggregationService,
            IPostRepository postRepository)
        {
            this._autoMapperService = autoMapperService;

            this._validationsProvider = validationsProvider;
            this._commentAggregationService = commentAggregationService;
            this._postRepository = postRepository;
        }

        public async Task<ServiceResult<PostItemBo>> GetPostByIdAsync(string id)
        {
            if (id.IsNullOrEmpty())
            {
                return new ServiceResult<PostItemBo> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._postRepository.GetByIdAsync(id);

            var mapperResult = this._autoMapperService.Map<PostItemBo>(repositoryResult);

            await this._commentAggregationService.AggregateAsync(mapperResult);

            return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<PostsResponseBo>> GetPostsAsync(int skip, int limit)
        {
            var pagingOptions = new PagingOptions().SkipItems(skip).LimitItems(limit);
            var sortingOptions = new SortingOptions("CreatedOn");

            var sortOptions = new List<SortingOptions> { sortingOptions };

            var repositoryResult = await this._postRepository.GetAsync(post => post.IsDeleted == false, pagingOptions, sortOptions);

            var mapperResult = this._autoMapperService.Map<IEnumerable<PostItemBo>>(repositoryResult);

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

            var repositoryResult = await this._postRepository.CreateAsync(this._autoMapperService.Map<PostEntity>(createPostRequestBo));

            var mapperResult = this._autoMapperService.Map<PostItemBo>(repositoryResult);

            return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<bool>> DeletePostAsync(string id)
        {
            if (id.IsNullOrEmpty())
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
                Skip = skip + limit >= postsCount ? (int?)null : skip + limit,
                Limit = limit
            };

            return postsResponseBo;
        }
    }
}