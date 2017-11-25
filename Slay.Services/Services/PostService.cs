using FluentValidation;
using Slay.Models.BusinessObjects.Post;
using Slay.ServicesContracts.Services;
using Slay.Utilities.ServiceResult;
using System.Threading.Tasks;

namespace Slay.Services.Services
{
	public sealed class PostService : IPostService
    {
	    private readonly IValidator<CreatePostRequestBo> createPostValidator;

	    public PostService(IValidator<CreatePostRequestBo> createPostValidator)
	    {
		    this.createPostValidator = createPostValidator;
	    }

        public Task<ServiceResult<PostResponseBo>> GetPostByIdAsync(string id)
        {
            return null;
        }

        public async Task<ServiceResult<PostResponseBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = await this.createPostValidator.ValidateAsync(createPostRequestBo);

	        if (!validationResult.IsValid)
	        {
		        return null;
	        }

            return null;
        }
    }
}