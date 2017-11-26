using System.Collections.Generic;
using Slay.Models.DataTransferObjects.Comment;

namespace Slay.Models.DataTransferObjects.Post
{
	public sealed class PostResponseDto
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string Type { get; set; }

		public string Content { get; set; }

		public string Category { get; set; }

		public IEnumerable<string> Tags { get; set; }

		public IEnumerable<string> SearchTags { get; set; }

		public IEnumerable<CommentResponseDto> Comments { get; set; }

		public bool IsAnonymous { get; set; }

		public string CreatedBy { get; set; }

		public string CreatedOn { get; set; }
	}
}