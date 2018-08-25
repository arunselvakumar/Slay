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
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationsProvider"/> class.
        /// </summary>
        /// <param name="createPostValidator">The create post validator.</param>
        /// <param name="createCommentValidator">The create comment validator.</param>
        /// <param name="createCategoryValidator">The create category validator.</param>
        /// <param name="postUploadValidator">The post upload validator.</param>
        /// <param name="templateUploadValidator">The template upload validator.</param>
        public ValidationsProvider(
            IValidator<CreatePostRequestBo> createPostValidator,
            IValidator<CreateCommentRequestBo> createCommentValidator,
            IValidator<CreateCategoryRequestBo> createCategoryValidator,
            IValidator<PostUploadRequestContext> postUploadValidator,
            IValidator<TemplateUploadRequestContext> templateUploadValidator)
        {
            this.CreatePostValidator = createPostValidator;
            this.CreateCommentValidator = createCommentValidator;
            this.CreateCategoryValidator = createCategoryValidator;
            this.PostUploadValidator = postUploadValidator;
            this.TemplateUploadValidator = templateUploadValidator;
        }

        public IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        public IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }

        public IValidator<CreateCategoryRequestBo> CreateCategoryValidator { get; }

        public IValidator<PostUploadRequestContext> PostUploadValidator { get; set; }

        public IValidator<TemplateUploadRequestContext> TemplateUploadValidator { get; set; }
    }
}