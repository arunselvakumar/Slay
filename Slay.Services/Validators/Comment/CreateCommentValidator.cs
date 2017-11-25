using FluentValidation;
using Slay.Models.BusinessObjects.Comment;

namespace Slay.Services.Validators.Comment
{
	public sealed class CreateCommentValidator : AbstractValidator<CreateCommentRequestBo>
	{
		private readonly string _commentShouldNotBeEmpty = "COMMENT_SHOULDNOTBEEMPTY_ERROR";

		private readonly string _commentLengthShouldBeLessThan9999Error = "COMMENT_LENGTHSHOULDBELESSTHAN999_ERROR";

		public CreateCommentValidator()
		{
			this.RuleFor(request => request.Comment)
				.NotEmpty().WithMessage(_commentShouldNotBeEmpty)
				.Length(1, 9999).WithMessage(_commentLengthShouldBeLessThan9999Error);
		}
	}
}