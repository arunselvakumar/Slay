namespace Slay.Services.Validators.Post
{
    using CSharpVerbalExpressions;

    using FluentValidation;

    using Slay.Models.BusinessObjects.Post;
    using Slay.Models.Enums;

    public sealed class CreatePostValidator : AbstractValidator<CreatePostRequestBo>
    {
        private readonly string _postContentShouldBeLessThanCharacters999Error = "CONTENT_LENGTHSHOULDBELESSTHAN999_ERROR";

        private readonly string _postContentShouldBeValidUrlError = "CONTENT_SHOULDBEVALIDURL_ERROR";

        private readonly string _postContentShouldNotBeEmptyError = "CONTENT_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postTitleShouldBeLessThan999CharactersError = "TITLE_LENGTHSHOULDBELESSTHAN999_ERROR";

        private readonly string _postTitleShouldNotBeEmptyError = "TITLE_SHOULDNOTBEEMPTY_ERROR";

        private readonly string _postTypeShouldBeValidError = "TYPE_SHOULDBEVALID_ERROR";

        private readonly string _postTypeShouldNotBeEmptyError = "TYPE_SHOULDNOTBEEMPTY_ERROR";

        public CreatePostValidator()
        {
            this.RuleFor(request => request.Title).NotEmpty().WithMessage(this._postTitleShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._postTitleShouldBeLessThan999CharactersError);

            this.RuleFor(request => request.Type).NotEmpty().WithMessage(this._postTypeShouldNotBeEmptyError)
                .NotEqual(PostTypeEnum.None).WithMessage(this._postTypeShouldBeValidError);

            this.RuleFor(request => request.Content).NotEmpty().WithMessage(this._postContentShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._postContentShouldBeLessThanCharacters999Error);

            this.RuleFor(request => request).Must(this.ValidUrlContent).WithMessage(this._postContentShouldBeValidUrlError);
        }

        private bool ValidUrlContent(CreatePostRequestBo post)
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