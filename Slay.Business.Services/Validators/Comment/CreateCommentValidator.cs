namespace Slay.Business.Services.Validators.Comment
{
    using FluentValidation;

    using Slay.Models.BusinessObjects.Comment;

    public sealed class CreateCommentValidator : AbstractValidator<CreateCommentRequestBo>
    {
        private readonly string _commentLengthShouldBeLessThan999CharactersError =
            "COMMENT_LENGTHSHOULDBELESSTHAN999_ERROR";

        private readonly string _commentShouldNotBeEmptyError = "COMMENT_SHOULDNOTBEEMPTY_ERROR";

        public CreateCommentValidator()
        {
            this.RuleFor(request => request.Comment).NotEmpty().WithMessage(this._commentShouldNotBeEmptyError)
                .Length(1, 999).WithMessage(this._commentLengthShouldBeLessThan999CharactersError);
        }
    }
}