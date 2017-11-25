using FluentValidation;
using Slay.Models.BusinessObjects.Post;
using Slay.ServicesContracts.Providers.ValidationsProviders;

namespace Slay.Services.Providers.ValidationsProviders
{
	public sealed class PostValidationsProvider : IPostValidationsProvider
	{
		public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

		public PostValidationsProvider(IValidator<CreatePostRequestBo> createPostValidator)
		{
			this.CreatePostValidator = createPostValidator;
		}
	}
}