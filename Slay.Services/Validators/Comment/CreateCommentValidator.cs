using FluentValidation;
using Slay.Models.BusinessObjects.Comment;

namespace Slay.Services.Validators.Comment
{
	public sealed class CreateCommentValidator : AbstractValidator<CreateCommentRequestBo>
	{
		private readonly string _commentShouldNotBeEmptyError = "COMMENT_SHOULDNOTBEEMPTY_ERROR";

		private readonly string _commentLengthShouldBeLessThan999CharactersError = "COMMENT_LENGTHSHOULDBELESSTHAN999_ERROR";

		public CreateCommentValidator()
		{
			this.RuleFor(request => request.Comment)
				.NotEmpty().WithMessage(_commentShouldNotBeEmptyError)
				.Length(1, 999).WithMessage(_commentLengthShouldBeLessThan999CharactersError);
		}
	}
}