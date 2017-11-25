using FluentValidation;
using Slay.Models.BusinessObjects.Comment;
using Slay.Models.BusinessObjects.Post;

namespace Slay.ServicesContracts.Providers.ValidationsProviders
{
	public interface IValidationsProvider
	{
		IValidator<CreatePostRequestBo> CreatePostValidator { get; }

		IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }
	}
}