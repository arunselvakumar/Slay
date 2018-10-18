namespace Slay.Business.Services.Validators.Post
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using CSharpVerbalExpressions;

    using FluentValidation;

    using Slay.Business.ServicesContracts.Services;
    using Slay.Models.BusinessObjects.Post;
    using Slay.Models.Enums;

    public sealed class CreatePostValidator : AbstractValidator<CreatePostRequestBo>
    {
        private readonly string _postContentShouldBeLessThanCharacters999Error = "POST_CONTENT_LENGTHSHOULDBELESSTHAN999_ERROR";

        private readonly string _postContentShouldBeValidUrlError = "POST_CONTENT_SHOULDBEVALIDURL_ERROR";

        private readonly string _postContentShouldNotBeEmptyError = "POST_CONTENT_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postTitleShouldBeLessThan999CharactersError = "POST_TITLE_LENGTHSHOULDBELESSTHAN999_ERROR";

        private readonly string _postTitleShouldNotBeEmptyError = "POST_TITLE_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postTypeShouldBeValidError = "POST_TYPE_SHOULDBEVALID_ERROR";

        private readonly string _postTypeShouldNotBeEmptyError = "POST_TYPE_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postCategoryShouldBeValidError = "POST_CATEGORY_SHOULDBEVALID_ERROR";

        private readonly string _postCategoryShouldNotBeEmptyError = "POST_CATEGORY_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postCategoryShouldBeLessThanCharacters999Error = "POST_CATEGORY_LENGTHSHOULDBELESSTHAN999_ERROR";


        private readonly IPostCategoryService _postCategoryService;

        public CreatePostValidator(IPostCategoryService postCategoryService)
        {
            this._postCategoryService = postCategoryService;

            this.ApplyRules();
        }

        private void ApplyRules()
        {
            this.RuleFor(request => request.Title).NotEmpty().WithMessage(this._postTitleShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._postTitleShouldBeLessThan999CharactersError);

            this.RuleFor(request => request.Content).NotEmpty().WithMessage(this._postContentShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._postContentShouldBeLessThanCharacters999Error);

            this.RuleFor(request => request.Category).NotEmpty().WithMessage(this._postCategoryShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._postCategoryShouldBeLessThanCharacters999Error);

            this.RuleFor(request => request.Type).NotEmpty().WithMessage(this._postTypeShouldNotBeEmptyError)
                .NotEqual(PostTypeEnum.None).WithMessage(this._postTypeShouldBeValidError);

            this.RuleFor(request => request).Must(this.IsValidUrlContent).WithMessage(this._postContentShouldBeValidUrlError);

            this.RuleFor(request => request).Must(this.IsValidCategory).WithMessage(this._postCategoryShouldBeValidError);
        }

        private bool IsValidCategory(CreatePostRequestBo post)
        {
            var validCategories = this._postCategoryService.GetCategoriesAsync(default(CancellationToken)).Result;

            return validCategories.Value.Categories.Any(category => string.Equals(category.Id, post.Category, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool IsValidUrlContent(CreatePostRequestBo post)
        {
            if (post.Type == PostTypeEnum.Image)
            {
                var urlExpression = new VerbalExpressions().StartOfLine().Then("http").Maybe("s").Then("://")
                    .Maybe("www.").AnythingBut(" ").EndOfLine();

                return urlExpression.IsMatch(post.Content);
            }

            return true;
        }
    }
}