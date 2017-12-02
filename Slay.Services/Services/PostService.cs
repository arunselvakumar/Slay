using AutoMapper;
using Slay.DalContracts.Repositories;
using Slay.Models.BusinessObjects.Post;
using Slay.ServicesContracts.Services;
using Slay.Utilities.ServiceResult;
using System.Threading.Tasks;
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

        public async Task<ServiceResult<PostResponseBo>> GetPostByIdAsync(string id)
        {
	        if (string.IsNullOrEmpty(id))
	        {
				return new ServiceResult<PostResponseBo> { Errors = new[] { new Error { Code = "POSTID_MANDATORY_ERROR" } } };
			}

	        var repositoryResult = await this._postRepository.GetByIdAsync(id);

	        var mapperResult = this._mapper.Map<PostResponseBo>(repositoryResult);

			return new ServiceResult<PostResponseBo> { Value = mapperResult };
        }

        public async Task<ServiceResult<PostResponseBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = await this._validationsProvider.CreatePostValidator.ValidateAsync(createPostRequestBo);

	        if (!validationResult.IsValid)
	        {
		        return new ServiceResult<PostResponseBo> { Errors = validationResult.Errors.ToServiceResultErrors() };
	        }

	        var repositoryResult = await this._postRepository.CreateAsync(this._mapper.Map<PostEntity>(createPostRequestBo));

	        var mapperResult = this._mapper.Map<PostResponseBo>(repositoryResult);

			return new ServiceResult<PostResponseBo> { Value = mapperResult };
        }
    }
}