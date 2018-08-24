namespace Slay.Business.Services.Providers.ValidationsProviders
{
    using FluentValidation;

    using Slay.Business.ServicesContracts.Providers.ValidationsProviders;
    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Post;
    
    public sealed class ValidationsProvider : IValidationsProvider
    {
        public ValidationsProvider(
            IValidator<CreatePostRequestBo> createPostValidator,
            IValidator<CreateCommentRequestBo> createCommentValidator,
            IValidator<CreateCategoryRequestBo> createCategoryValidator,
            IValidator<PostUploadRequestContext> fileUploadValidator)
        {
            this.CreatePostValidator = createPostValidator;
            this.CreateCommentValidator = createCommentValidator;
            this.CreateCategoryValidator = createCategoryValidator;
            this.FileUploadValidator = fileUploadValidator;
        }

        public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        public IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }

        public IValidator<CreateCategoryRequestBo> CreateCategoryValidator { get; }

        public IValidator<PostUploadRequestContext> FileUploadValidator { get; set; }
    }
}