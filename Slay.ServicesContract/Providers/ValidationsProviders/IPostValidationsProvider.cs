using FluentValidation;
using Slay.Models.BusinessObjects.Post;

namespace Slay.ServicesContracts.Providers.ValidationsProviders
{
	public interface IPostValidationsProvider
	{
		IValidator<CreatePostRequestBo> CreatePostValidator { get; }
	}
}