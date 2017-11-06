using FluentValidation;
using Slay.Models.BOs.Post;

namespace Slay.Validators.Post
{
    public sealed class CreatePostValidator : AbstractValidator<CreatePostRequestBo>
    {
        private readonly string postTitleEmptyError = "posttitleempty_error";

        private readonly string postTitleShouldBeLessThan200Error = "posttitleshouldbelessthan200_error";

        public CreatePostValidator()
        {
            this.RuleFor(request => request.Title)
                .NotEmpty().WithMessage(this.postTitleEmptyError)
                .Length(1, 200).WithMessage(this.postTitleShouldBeLessThan200Error);

            this.RuleFor(request => request.Type).NotEmpty();
            this.RuleFor(request => request.Content).NotEmpty();
            this.RuleFor(request => request.Category).NotEmpty();
        }
    }
}
