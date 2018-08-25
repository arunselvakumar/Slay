namespace Slay.Business.Services.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using Microsoft.WindowsAzure.Storage.Blob;

    using Slay.Business.ServicesContracts.Aggregators;
    using Slay.Business.ServicesContracts.Facades;
    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Business.ServicesContracts.Services;
    using Slay.DalContracts.Options;
    using Slay.DalContracts.Repositories;
    using Slay.Models.BusinessObjects.File;
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

        private readonly IAzureStorageServicesFacade _azureStorageServicesFacade;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostService"/> class.
        /// </summary>
        /// <param name="autoMapperService">The automatic mapper service.</param>
        /// <param name="validationsProvider">The validations provider.</param>
        /// <param name="commentAggregationService">The comment aggregation service.</param>
        /// <param name="azureStorageServicesFacade">The azure storage services facade.</param>
        /// <param name="postRepository">The post repository.</param>
        public PostService(
            IMapper autoMapperService, 
            IValidationsProvider validationsProvider,
            ICommentAggregationService commentAggregationService,
            IAzureStorageServicesFacade azureStorageServicesFacade,
            IPostRepository postRepository)
        {
            this._autoMapperService = autoMapperService;

            this._validationsProvider = validationsProvider;
            this._commentAggregationService = commentAggregationService;

            this._azureStorageServicesFacade = azureStorageServicesFacade;

            this._postRepository = postRepository;
        }

        public async Task<ServiceResult<PostItemBo>> GetPostByIdAsync(string id, CancellationToken token)
        {
            if (id.IsNullOrEmpty())
            {
                return new ServiceResult<PostItemBo> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
            }

            var repositoryResult = await this._postRepository.GetByIdAsync(id, token);

            var mapperResult = this._autoMapperService.Map<PostItemBo>(repositoryResult);

            await this._commentAggregationService.AggregateAsync(mapperResult, token);

            return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<PostsListResponseBo>> GetPostsAsync(int skip, int limit, CancellationToken token)
        {
            var pagingOptions = new PagingOptions().SkipItems(skip).LimitItems(limit);
            var sortingOptions = new SortingOptions("CreatedOn");

            var sortOptions = new List<SortingOptions> { sortingOptions };

            var repositoryResult = await this._postRepository.GetAsync(post => post.IsDeleted == false, pagingOptions, sortOptions, token);

            var mapperResult = this._autoMapperService.Map<IEnumerable<PostItemBo>>(repositoryResult);

            var postsResponseBo = await this.MapPostsResultsWithPageOptions(skip, limit, mapperResult, token);

            return new ServiceResult<PostsListResponseBo> { Value = postsResponseBo };
        }

        public async Task<ServiceResult<PostItemBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.CreatePostValidator.ValidateAsync(createPostRequestBo, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<PostItemBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var repositoryResult = await this._postRepository.CreateAsync(this._autoMapperService.Map<PostEntity>(createPostRequestBo), token);

            var mapperResult = this._autoMapperService.Map<PostItemBo>(repositoryResult);

            return new ServiceResult<PostItemBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<bool>> DeletePostAsync(string id, CancellationToken token)
        {
            if (id.IsNullOrEmpty())
            {
                return new ServiceResult<bool> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
            }

            var result = await this._postRepository.DeleteAsync(id, token);

            return new ServiceResult<bool> { Value = result };
        }

        public async Task<ServiceResult<PostUploadResponseContext>> UploadPostAsync(PostUploadRequestContext uploadRequestContext, CancellationToken token)
        {
            var validationResult = await this._validationsProvider.PostUploadValidator.ValidateAsync(uploadRequestContext, token);

            if (!validationResult.IsValid)
            {
                return new ServiceResult<PostUploadResponseContext> { Errors = validationResult.Errors.ToServiceResultErrors() };
            }

            var azureStorageResult = await this._azureStorageServicesFacade.SaveBlobInContainerAsync(uploadRequestContext, token);

            if (azureStorageResult.IsNull())
            {
                return new ServiceResult<PostUploadResponseContext> { Errors = new[] { new Error { Code = "POST_UPLOADFAILED_ERROR" } } };
            }

            var mapperResult = this._autoMapperService.Map<CloudBlockBlob, PostUploadResponseContext>(azureStorageResult);

            return new ServiceResult<PostUploadResponseContext> { Value = mapperResult };
        }

        private async Task<PostsListResponseBo> MapPostsResultsWithPageOptions(int skip, int limit, IEnumerable<PostItemBo> mapperResult, CancellationToken token)
        {
            var postsCount = await this._postRepository.CountAsync(postEntity => postEntity.IsDeleted == false, token);

            var postsResponseBo = new PostsListResponseBo
            {
                Posts = mapperResult,
                Skip = skip + limit >= postsCount ? (int?)null : skip + limit,
                Limit = limit
            };

            return postsResponseBo;
        }
    }
}