using FluentValidation;

using Slay.BusinessObjects.Post;

namespace Slay.Services.Validators.Post
{
	public sealed class CreatePostValidator : AbstractValidator<CreatePostRequestBo>
    {
        private readonly string _postTitleEmptyError = "posttitleempty_error";

        private readonly string _postTitleShouldBeLessThan200Error = "posttitleshouldbelessthan200_error";

        public CreatePostValidator()
        {
            this.RuleFor(request => request.Title)
                .NotEmpty().WithMessage(this._postTitleEmptyError)
                .Length(1, 200).WithMessage(this._postTitleShouldBeLessThan200Error);

            this.RuleFor(request => request.Type).NotEmpty();
            this.RuleFor(request => request.Content).NotEmpty();
            this.RuleFor(request => request.Category).NotEmpty();
        }
    }
}