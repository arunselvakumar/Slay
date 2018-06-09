namespace Slay.Business.Services.Providers.ValidationsProviders
{
    using FluentValidation;

    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.Post;
    
    public sealed class ValidationsProvider : IValidationsProvider
    {
        public ValidationsProvider(
            IValidator<CreatePostRequestBo> createPostValidator,
            IValidator<CreateCommentRequestBo> createCommentValidator)
        {
            this.CreatePostValidator = createPostValidator;
            this.CreateCommentValidator = createCommentValidator;
        }

        public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        public IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }
    }
}