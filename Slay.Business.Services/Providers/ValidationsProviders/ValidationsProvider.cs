namespace Slay.Business.Services.Providers.ValidationsProviders
{
    using FluentValidation;

    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.Post;
    
    public sealed class ValidationsProvider : IValidationsProvider
    {
        public ValidationsProvider(
            IValidator<CreatePostRequestBo> createPostValidator,
            IValidator<CreateCommentRequestBo> createCommentValidator,
            IValidator<CreateCategoryRequestBo> createCategoryValidator)
        {
            this.CreatePostValidator = createPostValidator;
            this.CreateCommentValidator = createCommentValidator;
            this.CreateCategoryValidator = createCategoryValidator;
        }

        public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        public IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }

        public IValidator<CreateCategoryRequestBo> CreateCategoryValidator { get; }
    }
}