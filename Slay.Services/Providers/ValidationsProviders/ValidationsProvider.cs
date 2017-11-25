using FluentValidation;
using Slay.Models.BusinessObjects.Comment;
using Slay.Models.BusinessObjects.Post;
using Slay.ServicesContracts.Providers.ValidationsProviders;

namespace Slay.Services.Providers.ValidationsProviders
{
	public sealed class ValidationsProvider : IValidationsProvider
	{
		public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

		public IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }

		public ValidationsProvider(IValidator<CreatePostRequestBo> createPostValidator, IValidator<CreateCommentRequestBo> createCommentValidator)
		{
			this.CreatePostValidator = createPostValidator;
			this.CreateCommentValidator = createCommentValidator;
		}
	}
}