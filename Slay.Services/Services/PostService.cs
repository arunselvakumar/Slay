using Slay.BusinessObjects.Post;
using Slay.Services.Validators.Post;
using Slay.ServicesContracts.Services;
using Slay.Utilities.ServiceResult;

using System.Threading.Tasks;
using FluentValidation;

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

        public Task<ServiceResult<PostResponseBo>> CreatePostAsync(CreatePostRequestBo createPostRequestBo)
        {
            var validationResult = this.createPostValidator.ValidateAsync(createPostRequestBo);

            return null;
        }
    }
}