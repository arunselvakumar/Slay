namespace Slay.Business.ServicesContracts.Providers.ValidationsProviders
{
    using FluentValidation;

    using Slay.Models.BusinessObjects.Category;
    using Slay.Models.BusinessObjects.Comment;
    using Slay.Models.BusinessObjects.File;
    using Slay.Models.BusinessObjects.Post;

    public interface IValidationsProvider
    {
        IValidator<CreatePostRequestBo> CreatePostValidator { get; }

        IValidator<CreateCommentRequestBo> CreateCommentValidator { get; }

        IValidator<CreateCategoryRequestBo> CreateCategoryValidator { get; }

        IValidator<PostUploadRequestContext> FileUploadValidator { get; set; }
    }
}